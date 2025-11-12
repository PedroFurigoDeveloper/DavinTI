namespace DavinTI.Domain.Entities {
    public class Telefone {
        public int Id { get; private set; }
        public int IdContato { get; private set; }
        public string Numero { get; private set; } = string.Empty;

        public Contato Contato { get; private set; } = null!;

        public Telefone() { }

        public Telefone(int idContato, string numero, int id) {
            DefinirContato(idContato);
            AtualizarNumero(numero);
            Id = id;
        }
        public Telefone(int idContato, string numero) {
            DefinirContato(idContato);
            AtualizarNumero(numero);
        }

        public void AtualizarNumero(string numero) {
            if (string.IsNullOrWhiteSpace(numero))
                throw new ArgumentException("O número de telefone não pode ser vazio.");

            if (numero.Length > 16)
                throw new ArgumentException("O número de telefone não pode ultrapassar 16 caracteres.");

            Numero = numero;
        }

        public void DefinirContato(int idContato) {
            if (idContato <= 0)
                throw new ArgumentException("Id do contato inválido.");

            IdContato = idContato;
        }

        public void Atualizar(string numero) {
            AtualizarNumero(numero);
        }
    }
}
