using System;
using System.Collections.Generic;

namespace FRONT_web_personal_saving.Models;

public partial class Usuario
{
    public int id_usuario { get; set; }

    public string nombre { get; set; } = null!;

    public string email { get; set; } = null!;

    public string contraseña { get; set; } = null!;

   
}
