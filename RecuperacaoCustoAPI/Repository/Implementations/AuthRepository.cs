using RecuperacaoCustoAPI.Exceptions;
using RecuperacaoCustoAPI.Models;
using RecuperacaoCustoAPI.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using RecuperacaoCustoAPI.DTO;
using RecuperacaoCustoAPI.Mapping.Interfaces;
using System.Text;

namespace RecuperacaoCustoAPI.Repository.Implementations
{
    public class AuthRepository : IAuthRepository
    {
        private DbContext _db;
        private readonly IMapper<Usuario, UsuarioDTO> _mapper;
        public AuthRepository(DbContext db, IMapper<Usuario, UsuarioDTO> mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public UsuarioDTO AlterarUsuario(UsuarioDTO usuario)
        {
            Usuario usuarioAntigo = _db.Set<Usuario>().Find(usuario.Login);
            if (usuarioAntigo == null) throw new NaoEncontradoException<Usuario>("Usuário não encontrado!");

            Usuario novoUsuario = _mapper.Map(usuario);

            novoUsuario.Senha = usuarioAntigo.Senha;

            _db.Entry(usuarioAntigo).State = EntityState.Detached;
            _db.Entry(novoUsuario).State = EntityState.Modified;
            _db.SaveChanges();
            return _mapper.Map(novoUsuario);
            
        }

        public void AlteraSenha(string login, string novaSenha)
        {
            Usuario user = _db.Set<Usuario>().Find(login);
            if (user == null) throw new NaoEncontradoException<Usuario>("Usuário não encontrado!");

            user.Senha = novaSenha;
            _db.SaveChanges();
        }

        public bool Checkuser(string login, string senha)
        {
            Usuario user = _db.Set<Usuario>().Find(login);
            if (user == null) throw new NaoEncontradoException<Usuario>("Usuário não encontrado!");

            return user.Senha == senha;
        }

        public string CriarUsuario(UsuarioDTO usuario)
        {
            if (Existe(usuario.Login)) throw new ConflitoException<Usuario>("Usuário já cadastrado!");
            Usuario userBD = _mapper.Map(usuario);
            Random rnd = new Random();
            string senha = userBD.Login + rnd.Next(100, 1000);
            userBD.Senha = Convert.ToBase64String(Encoding.UTF8.GetBytes(senha));

            _db.Set<Usuario>().Add(userBD);

            _db.SaveChanges();
            return senha;

        }

        public Usuario RecuperarSenha(string login)
        {
            Usuario user = _db.Set<Usuario>().Find(login);
            if (user == null) throw new NaoEncontradoException<Usuario>("Usuário não encontrado!");
            return user;
        }

        private bool Existe(string login)
        {
            return _db.Set<Usuario>().Any(x => x.Login == login);
        }
    }
}