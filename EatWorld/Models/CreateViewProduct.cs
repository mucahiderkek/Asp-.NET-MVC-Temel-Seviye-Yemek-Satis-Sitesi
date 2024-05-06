using System.ComponentModel.DataAnnotations;

namespace EatWorld.Models
{
    public class CreateViewProduct
    {

		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "Yemek Adı Yazınız")]
		[Display(Name = "Yemek Adı")]
		public string? Adı { get; set; }

		[Required(ErrorMessage = "Yemek Açıklaması Yazınız")]
		[Display(Name = "Açıklaması")]
		public string? Açıklaması { get; set; }

		[Required(ErrorMessage = "Yemek Kategorisi Yazınız")]
		[Display(Name = "Kategori")]
		public string? Kategorisi { get; set; }

		[Required(ErrorMessage = "Yemek Fiyatı Yazınız")]
		[Display(Name = "Fiyatı")]
		public float? Fiyatı { get; set; }

		[Required(ErrorMessage = "Ürün Resmini seçiniz.")]
        public IFormFile Photo { get; set; }
        public string? PhotoFileName { get; set; } //photopathaynı görevi görüyor
    }
}
