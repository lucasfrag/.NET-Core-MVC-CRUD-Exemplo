using System.Collections.Generic;
using CRUD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUD.Controllers
{
    public class UsuarioController : Controller
    {

        public IActionResult Lista()
        {
            if(HttpContext.Session.GetInt32("IdUsuario") == null)
                return RedirectToAction("Login");
            
            UsuarioBanco usuarioBanco = new UsuarioBanco();
            List<Usuario> Lista = usuarioBanco.Listar();
            
            return View(Lista);            
        }

        public IActionResult Cadastro()
        {
            if(HttpContext.Session.GetInt32("IdUsuario") == null)
                return RedirectToAction("Login");
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(Usuario usuario)
        {
            UsuarioBanco usuarioBanco = new UsuarioBanco();
            usuarioBanco.Inserir(usuario);
            ViewBag.Mensagem = "Cadastro efetuado com sucesso!";
            return View();
        }

        public IActionResult Editar(int Id)
        {
            if(HttpContext.Session.GetInt32("IdUsuario") == null)
                return RedirectToAction("Login");
            

            UsuarioBanco usuarioBanco = new UsuarioBanco();
            Usuario usuario = usuarioBanco.BuscarPorId(Id);
            return View(usuario);
        }

        [HttpPost]
        public IActionResult Atualizar(Usuario usuario)
        {
            UsuarioBanco usuarioBanco = new UsuarioBanco();
            usuarioBanco.Atualizar(usuario);
            ViewBag.Mensagem = "Usuario atualizado com sucesso!";
            return RedirectToAction("Lista");
        }

        public IActionResult Remover(int Id)
        {
            UsuarioBanco usuarioBanco = new UsuarioBanco();
            usuarioBanco.Remover(Id);
            return RedirectToAction("Lista");
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Usuario usuario)
        {
            UsuarioBanco usuarioBanco = new UsuarioBanco();
            Usuario usuarioSessao = usuarioBanco.VerificarDados(usuario);

            if(usuarioSessao != null)
            {
                ViewBag.Mensagem = "Você está logado!";
                HttpContext.Session.SetInt32("IdUsuario", usuarioSessao.Id);
                HttpContext.Session.SetString("NomeUsuario", usuarioSessao.Nome);

                return Redirect("Cadastro");
            } else {
                ViewBag.Mensagem = "Falha no login!";
                return View();
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Login");
        }
    }
}