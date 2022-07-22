using System;

namespace RCVUcabBackend.Exceptions
{
    public class ExcepcionTaller:Exception
    {
        public string Mensaje { get; set; }
        
        public ExcepcionTaller(string _mensaje) 
        {
            Mensaje = _mensaje;
        }

    }
}