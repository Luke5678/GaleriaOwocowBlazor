using System.ComponentModel.DataAnnotations;
using GaleriaOwocowBlazor.Shared;
using Microsoft.AspNetCore.Components.Forms;

namespace GaleriaOwocowBlazor.Client.Models
{
    public class FruitCreateFormModel
    {
        [Required] public string Name { get; set; }
        [Required] public string Color { get; set; }
        [Required] public string Description { get; set; }
        public IBrowserFile ImageFile { get; set; }

        public FruitCreateFormModel()
        {
        }

        public FruitCreateFormModel(Fruit fruit)
        {
            Name = fruit.Name;
            Color = fruit.Color;
            Description = fruit.Description;
        }

        public void FillMultipartContent(MultipartFormDataContent content)
        {
            content.Add(new StringContent(Name), "Name");
            content.Add(new StringContent(Color), "Color");
            content.Add(new StringContent(Description), "Description");
        }
    }
}
