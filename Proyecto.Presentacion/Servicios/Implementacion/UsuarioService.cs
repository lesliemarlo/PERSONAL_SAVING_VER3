
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using FRONT_web_personal_saving.Models;
using FRONT_web_personal_saving.Servicios.Contrato;

namespace FRONT_web_personal_saving.Servicios.Implementacion
{
    public class UsuarioService : IUsuarioService
    {

        //refrenencia a la BD
        private readonly BdPersonalSavingContext _dbContext;
        public UsuarioService(BdPersonalSavingContext dbContext)
        {
            _dbContext = dbContext;
        }
        //-------- devolver usuario
        public async Task<Usuario> GetUsuario(string email, string contraseña)
        {
            Usuario usuario_encontrado = await _dbContext.Usuario.Where(u => u.email == email && u.contraseña == contraseña)
                .FirstOrDefaultAsync();

            return usuario_encontrado;
        }
        //guardar usuario
        public async Task<Usuario> SaveUsuario(Usuario modelo)
        {
            _dbContext.Usuario.Add(modelo);
            await _dbContext.SaveChangesAsync();
            return modelo;
        }
    }
    //para poder usar este servicio debemos configurarlo en program
}
