using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MonografiasIfma.Models;

namespace MonografiasIfma.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserManager<Funcionario> _userManager;
        private readonly SignInManager<Funcionario> _signInManager;

        public LoginController(UserManager<Funcionario> userManager, SignInManager<Funcionario> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpPost]
        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registro(RegistroViewModel model)
        {

            if (ModelState.IsValid)
            {
                //copia os dados do RegistroViewModel para o funcionário
                var user = new Funcionario
                {
                    UserName = model.Login
                };

                //armazena os dados do usuário na tabela AspNetUsers
                var result = await _userManager.CreateAsync(user, model.Senha);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    model.Login, model.Senha, model.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Login Inválido");
            }
            return View(model);
        }
    }
}
