using Microsoft.AspNetCore.Mvc;
using EcomPainel.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EcomPainel.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl = "http://localhost:5124/api/Products"; // URL da API de produtos
        private readonly string _categoryApiUrl = "http://localhost:5124/api/Category"; // URL da API de categorias

        public ProductController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
            var products = await _httpClient.GetFromJsonAsync<Product[]>(_apiUrl);  // Chama a API para listar os produtos
            var categories = await _httpClient.GetFromJsonAsync<Category[]>(_categoryApiUrl);  // Chama a API para listar as categorias
            ViewBag.Categories = categories;  // Passa as categorias para a View
            return View(products);  // Passa a lista de produtos para a view
        }

        // GET: Product/Create
        public async Task<IActionResult> Create()
        {
            var categories = await _httpClient.GetFromJsonAsync<Category[]>(_categoryApiUrl);  // Busca as categorias
            ViewBag.Categories = categories;  // Passa as categorias para a view
            return View(new Product());  // Passa um novo produto (vazio) para a view
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                // Envia os dados para a API
                var response = await _httpClient.PostAsJsonAsync(_apiUrl, product);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));  // Redireciona após sucesso
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Erro ao criar produto.");
                }
            }
            var categories = await _httpClient.GetFromJsonAsync<Category[]>(_categoryApiUrl); // Recarrega as categorias em caso de erro
            ViewBag.Categories = categories; // Passa as categorias para a view
            return View(product);  // Se houver erro, retorna à view de criação com o modelo
        }
    }
}
