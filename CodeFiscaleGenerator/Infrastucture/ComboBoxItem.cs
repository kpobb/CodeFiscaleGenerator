namespace CodeFiscaleGenerator.Infrastucture
{
    public class ComboBoxItem
    {
        public ComboBoxItem(int id, string text)
        {
            Id = id;
            Text = text;
        }

        public int Id { get; private set; }
        private string Text { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}