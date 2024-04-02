namespace Icecream.Models
{
    class Flavor
    {

        public Guid Id { get; set; }
        public string Name { get; set; }

        public Flavor(Guid Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }
    }
}