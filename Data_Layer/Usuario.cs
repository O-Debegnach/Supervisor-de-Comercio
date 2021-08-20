using System;

namespace Data_Layer
{
    [Serializable]
    public class Usuario
    {

        private string _nombre, _contraseña;
        private bool _administrador = false;

        public Usuario() { }

        public Usuario(string nombre, string contraseña, bool administrador)
        {
            _nombre = nombre;
            _contraseña = contraseña;
            _administrador = administrador;
        }

        #region Propiedades
        public string Contraseña
        {
            get => _contraseña;
            set => _contraseña = value;
        }

        public string Nombre
        {
            get => _nombre;
            set => _nombre = value;
        }
        public bool Administrador 
        { 
            get => _administrador; 
            set => _administrador = value; 
        }

        #endregion
    }
}
