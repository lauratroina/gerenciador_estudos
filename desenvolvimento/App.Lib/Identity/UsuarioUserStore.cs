using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using App.Lib.Entity;
using App.Lib.Service;

namespace App.Lib.Identity
{
    /// <summary>
    /// Class that implements the key ASP.NET Identity user store iterfaces
    /// </summary>
    public class UsuarioUserStore : IUserStore<UsuarioIdentity>, 
                             IUserPasswordStore<UsuarioIdentity>
    {
        

        public UsuarioService service;

        public UsuarioUserStore(UsuarioService usuarioService = null)
        {
            service = usuarioService;
        }
        
        /// <summary>
        /// Insert a new IdentityUser in the UserTable
        /// </summary>
        /// <param name="user"></param>F
        /// <returns></returns>
        public Task CreateAsync(UsuarioIdentity user)
        {
            if (user == null) {
                throw new ArgumentNullException("user");
            }

            service.Salvar(user.CopyTo(new Usuario()));
            
            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Returns an IdentityUser instance based on a userId query 
        /// </summary>
        /// <param name="userId">The user's Id</param>
        /// <returns></returns>
        public Task<UsuarioIdentity> FindByIdAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("Null or empty argument: userId");
            }
            UsuarioIdentity result = null;
            var usuario = service.Carregar(Convert.ToInt32(userId));
            if (usuario != null)
            {
                result = usuario.CopyTo(new UsuarioIdentity());
            }

            return Task.FromResult<UsuarioIdentity>(result);
        }

        /// <summary>
        /// Returns an IdentityUser instance based on a userName query 
        /// </summary>
        /// <param name="userName">The user's name</param>
        /// <returns></returns>
        public Task<UsuarioIdentity> FindByNameAsync(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("Null or empty argument: userName");
            }

            UsuarioIdentity result = null;
            var usuario = service.Carregar(userName);
            if (usuario!=null)
            {
                result = usuario.CopyTo(new UsuarioIdentity());
            }
                

            return Task.FromResult<UsuarioIdentity>(result);
        }

        /// <summary>
        /// Updates the UsersTable with the IdentityUser instance values
        /// </summary>
        /// <param name="user">IdentityUser to be updated</param>
        /// <returns></returns>
        public Task UpdateAsync(UsuarioIdentity user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            var usuario = service.Carregar(user.ID);
            usuario.CopyFrom(user);
            service.Salvar(usuario);
            
            return Task.FromResult<object>(null);
        }

        public void Dispose()
        {
        }

        /// <summary>
        /// Deletes a user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task DeleteAsync(UsuarioIdentity user)
        {
            if (user != null)
            {
                var usuario = service.Carregar(user.ID);
                usuario.Inativo = true;
                service.Salvar(usuario);
            }

            return Task.FromResult<Object>(null);
        }

        /// <summary>
        /// Returns the PasswordHash for a given IdentityUser
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<string> GetPasswordHashAsync(UsuarioIdentity user)
        {
            string passwordHash = user.Senha;
            return Task.FromResult<string>(passwordHash);
        }

        /// <summary>
        /// Verifies if user has password
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<bool> HasPasswordAsync(UsuarioIdentity user)
        {
            var hasPassword = !string.IsNullOrEmpty(user.Senha);

            return Task.FromResult<bool>(hasPassword);
        }

        /// <summary>
        /// Sets the password hash for a given IdentityUser
        /// </summary>
        /// <param name="user"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        public Task SetPasswordHashAsync(UsuarioIdentity user, string passwordHash)
        {
            user.Senha = passwordHash;

            return Task.FromResult<Object>(null);
        }
    }
}
