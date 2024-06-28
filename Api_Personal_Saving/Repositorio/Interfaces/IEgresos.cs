using BACK_Api_Personal_Saving.Models;

namespace BACK_Api_Personal_Saving.Repositorio.Interfaces
{
    public interface IEgresos
    {
        IEnumerable<Egresos> listarEgresos();
        IEnumerable<EgresosO> listarEgresosO();
        EgresosO buscarEgreso(int id);
        string nuevoEgreso(EgresosO objE);
        //metodo para actualizar 
        string modificaEgreso(EgresosO objE);
        string eliminaEgreso(int id);
    }
}
