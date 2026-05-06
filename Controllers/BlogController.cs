using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Data;

namespace WebApplication1.Controllers
{
    public class BlogController : Controller
    {
        private readonly BlogDbContext _dbContext;
        
        // Конструктор получает DbContext через Dependency Injection
        public BlogController(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        // GET: Blog/AddPost - показывает форму
        [HttpGet]
        public IActionResult AddPost()
        {
            return View();
        }
        
        // POST: Blog/AddPost - сохраняет пост в БД
        [HttpPost]
        public IActionResult AddPost(Post post)
        {
            if (ModelState.IsValid)
            {
                // Устанавливаем дату создания
                post.CreatedAt = DateTime.Now;
                
                // Добавляем пост в БД
                _dbContext.Posts.Add(post);
                _dbContext.SaveChanges();  // Сохраняем изменения
                
                Console.WriteLine($"Пост сохранён в БД: {post.Title}");
                
                // Перенаправляем на список постов
                return RedirectToAction("Index", "Home");
            }
            
            // Если данные невалидны, показываем форму с ошибками
            return View(post);
        }
        
        // GET: Blog/GetAllPosts - для отладки, показывает все посты в консоли
        public IActionResult GetAllPosts()
        {
            var posts = _dbContext.Posts.ToList();
            
            Console.WriteLine($"\n=== ВСЕГО ПОСТОВ В БД: {posts.Count} ===");
            foreach (var post in posts)
            {
                Console.WriteLine($"[{post.Id}] {post.Title} - {post.Author} ({post.CreatedAt})");
            }
            Console.WriteLine("================================\n");
            
            return Content($"В базе {posts.Count} постов. Проверьте консоль для деталей.");
        }
        
        [HttpGet]
        public IActionResult Posts()
        {
            var posts = _dbContext.Posts
                .OrderByDescending(p => p.CreatedAt)  // Сначала новые
                .ToList();
            return View(posts);
        }
    }
}