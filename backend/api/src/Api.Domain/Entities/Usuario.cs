namespace Api.Domain.Entities
{
    public class Usuario
    {
        public int Secuencial { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Password { get; set; }
        public Usuario(int secuencial, string nombre, string correo, string password)
        {
            Secuencial = secuencial;
            Nombre = nombre;
            Correo = correo;
            Password = password;
        }
    }
}