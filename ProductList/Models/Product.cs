using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace ProductList.Models
{     
    public class Product
    {
        public static String[] CategoryTypes
        {
            get
            {
                return new String[] { "Electrical", "Equipment", "Building", "Cables" };
            }
        }

        public static String[] UnitTypes
        {
            get
            {
                return new String[] { "Item", "Meter", "Box", "Roll", "Sheet" };
            }
        }
       
       
        [Key]
        [StringLength(15)]
        [DisplayName("Product Code")]
        [RegularExpression(@"^[a - zA - Z0 - 9,.'@]+$ ")]
        public string ProductCode { get; set; }

        [Required(ErrorMessage = "Please enter product name.")]
        [DisplayName("Product Name")]
        [RegularExpression(@"^[a - zA - Z0 - 9,.'@]+$ ")]
        [StringLength(55)]                                 //using Excel formula "=LEN(cell) the longest existing name is 54 char
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Please enter category.")]
        [DisplayName("Category")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Please select unit description")]
        [DisplayName("Unit")]
        public string Unit { get; set; }


        [Required]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [RegularExpression(@"^\d+.\d{0,2}$", ErrorMessage = "Please enter a value with two decimal points.")]
        [DisplayName("Price")]
        public decimal ProductPrice { get; set; }
    }

    public class ProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ProductContext() : base("ProductList.Models.ProductContext")
        {
            Database.SetInitializer<ProductContext>(new DropCreateDatabaseIfModelChanges<ProductContext>());
            //default is: CreateDatabaseIfNotExists
            //recommended while in development is: DropCreateDatabaseAlways
        }
    }
}