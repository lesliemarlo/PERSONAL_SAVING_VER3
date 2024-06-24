
using Microsoft.EntityFrameworkCore;
using FRONT_web_personal_saving.Models;

namespace FRONT_web_personal_saving.Servicios.Contrato
{
    public interface IUsuarioService
    {
        //metodos
        //devuelve un usuario solo dependiende del emial y clave 
        Task<Usuario> GetUsuario(string email, string contraseña);
        //guardar usuario:
        Task<Usuario> SaveUsuario(Usuario modelo);

    }
}
