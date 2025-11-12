namespace DavinTI.Domain.Entities {
    public class Contato {
        public int IdContato { get; private set; }
        public string Nome { get; private set; } = string.Empty;
        public int Idade { get; private set; }

        public ICollection<Telefone> Telefones { get; private set; } = new List<Telefone>();

        protected Contato() { }

        public Contato(string nome, int idade) {
            SetNome(nome);
            SetIdade(idade);
        }

        public Contato(int idContato, string nome, int idade) {
            IdContato = idContato;
            SetNome(nome);
            SetIdade(idade);
        }

        public void Atualizar(string nome, int idade) {
            SetNome(nome);
            SetIdade(idade);
        }

        private void SetNome(string nome) {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("O nome do contato é obrigatório.", nameof(nome));

            if (nome.Length > 100)
                throw new ArgumentException("O nome não pode exceder 100 caracteres.", nameof(nome));

            Nome = nome.Trim();
        }

        private void SetIdade(int idade) {
            if (idade < 0)
                throw new ArgumentException("Idade não pode ser negativa.", nameof(idade));

            Idade = idade;
        }

        public void AdicionarTelefone(Telefone telefone) {
            if (telefone == null)
                throw new ArgumentNullException(nameof(telefone));

            Telefones.Add(telefone);
        }

        public void RemoverTelefone(Telefone telefone) {
            if (telefone == null)
                throw new ArgumentNullException(nameof(telefone));

            Telefones.Remove(telefone);
        }

        public void AtualizarNome(string nome) {
            SetNome(nome);
        }

        public void AtualizarIdade(int idade) {
            SetIdade(idade);
        }
    }
}
