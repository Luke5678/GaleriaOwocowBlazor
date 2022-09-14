using GaleriaOwocowBlazor.Shared;

namespace GaleriaOwocowBlazor.Server.Models
{
    public class FruitCreateRequestModel
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public IFormFile File { get; set; }

        public Fruit ToFruit()
        {
            return new Fruit
            {
                Name = Name, Color = Color, Description = Description, Image = File.FileName
            };
        }
    }
}
