using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using AuthProject.Data;
using AuthProject.Models;
// using MVC6Crud.Repository;
using AuthProject.ViewModel;

namespace AuthProject.Controllers
{
    
    public class UserController : Controller
    {
        // private IProductRepository _productRepository;
        private readonly ApplicationDbContext _context;
        public UserController(ApplicationDbContext context)
        {
            _context = context;
           
        }

        
        public IActionResult Index()
        {
            List<UserListViewModel> userListViewModelList = new List<UserListViewModel>();
            var userList = _context.Users;
          
            if (userList != null)
            {
                foreach (var item in userList)
                {
                    UserListViewModel userListViewModel = new UserListViewModel();
                    userListViewModel.Id = item.Id;
                    userListViewModel.Email = item.Email;
                    userListViewModel.Name = item.Name;
                    userListViewModelList.Add(userListViewModel);
                }
            }
            return View(userListViewModelList);           
        }

        public IActionResult Create()
        {
            UserViewModel userCreateViewModel = new UserViewModel();
            

            return View(userCreateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UserViewModel userCreateViewModel)
        {
            
            
            var user = new User()
            {
                Email = userCreateViewModel.Email,
                Password = userCreateViewModel.Password,
                Name = userCreateViewModel.Name,
                ConfirmPassword =userCreateViewModel.ConfirmPassword
            };
           
              if(user.Password !=user.ConfirmPassword ){
               TempData["SuccessMsg"] = "User password and ConfirmPassword not equal .";
                return RedirectToAction("Create");
              }
                _context.Users.Add(user);
                _context.SaveChanges();

                TempData["SuccessMsg"] = "User ("+ user.Name + ") added successfully.";
                return RedirectToAction("Index");
            
            
            return View(userCreateViewModel);
        }

        public IActionResult Edit(int? id)
        {
            var productToEdit = _context.Users.Find(id);
            if (productToEdit != null)
            {
                var userViewModel = new UserViewModel()
                {
                    Id = productToEdit.Id,
                    Email = productToEdit.Email,
                    Name = productToEdit.Name,
                    Password = productToEdit.Password,
                    ConfirmPassword = productToEdit.ConfirmPassword
                    
                
                };
                return View(userViewModel);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UserViewModel userViewModel)
        {
            
            var user = new User()
            {
                Id = userViewModel.Id,
                Email = userViewModel.Email,
                Name = userViewModel.Name,
                Password = userViewModel.Password,
                ConfirmPassword = userViewModel.ConfirmPassword
                
            };
            
                _context.Users.Update(user);
                _context.SaveChanges();
                TempData["SuccessMsg"] = "User (" + user.Name + ") updated successfully !";
                return RedirectToAction("Index");
            

            return View(userViewModel);
        }

        // public IActionResult Delete(int? id)
        // {
        //     var productToEdit = _context.Users.Find(id);
        //     if (productToEdit != null)
        //     {
        //         var productViewModel = new ProductViewModel()
        //         {
        //             Id = productToEdit.Id,
        //             Name = productToEdit.Name,
        //             Description = productToEdit.Description,
        //             Price = productToEdit.Price,
        //             CategoryId = productToEdit.CategoryId,
        //             Color = productToEdit.Color,
        //             Image = productToEdit.Image,
        //             Category = (IEnumerable<SelectListItem>)_context.Categories.Select(c => new SelectListItem()
        //             {
        //                 Text = c.CategoryName,
        //                 Value = c.CategoryId.ToString()
        //             })
        //         };
        //         return View(productViewModel);
        //     }
        //     else {
        //         return RedirectToAction("Index");
        //     }            
        // }

    //     [HttpPost]
    //     [ValidateAntiForgeryToken]
    //     public IActionResult DeleteProduct(int? id)
    //     {
    //         var product = _context.Products.Find(id);
    //         if (product == null)
    //         {
    //             return NotFound();
    //         }
    //         _context.Products.Remove(product);
    //         _context.SaveChanges();
    //         TempData["SuccessMsg"] = "Product (" + product.Name + ") deleted successfully.";
    //         return RedirectToAction("Index");
    //     }
    }
}
