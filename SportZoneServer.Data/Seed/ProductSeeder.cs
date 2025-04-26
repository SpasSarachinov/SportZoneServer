using SportZoneServer.Data.Entities;

namespace SportZoneServer.Data.Seed
{
    public static class ProductSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext db)
        {
            if (db.Products.Any())
            {
                return;
            }

            List<Category> categories = db.Categories.ToList();
            List<User> users = db.Users.ToList();

            
            List<Product> products =
            [
                new()
                {
                    Title = "Регулируеми дъмбели Bodyflex 2x15кг + допълнителен лост щанга",
                    Description = "",
                    MainImageUrl = "https://sportensklad.bg/image/cache/webp/catalog/FITNES_UREDI/tejesti/reguliruemi-dambeli-bodyflex-2x15kg-i-lost-adaptor----272x270.webp",
                    RegularPrice = 205m,
                    DiscountPercentage = 46,
                    DiscountedPrice = 109m,
                    Quantity = 400,
                    Rating = 5,
                    CategoryId = categories.First(c => c.Name == "Фитнес").Id,
                    SecondaryImages = new List<Image>
                    {
                        new Image { Uri = "https://sportensklad.bg/image/cache/webp/catalog/FITNES_UREDI/tejesti/reguliruemi-dambeli-bodyflex-2x15kg-i-lost-stanga%20_3_-74x74.webp" },
                        new Image { Uri = "https://sportensklad.bg/image/cache/webp/catalog/FITNES_UREDI/tejesti/reguliruemi-dambeli-bodyflex-2x15kg-i-lost-stanga%20_8_-74x74.webp" },
                        new Image { Uri = "https://sportensklad.bg/image/cache/webp/catalog/FITNES_UREDI/tejesti/reguliruemi-dambeli-bodyflex-2x15kg-i-lost-stanga%20_5_-74x74.webp" },
                        new Image { Uri = "https://sportensklad.bg/image/cache/webp/catalog/FITNES_UREDI/tejesti/reguliruemi-dambeli-bodyflex-2x15kg-i-lost-stanga%20_2_-74x74.webp" },
                        new Image { Uri = "https://sportensklad.bg/image/cache/webp/catalog/FITNES_UREDI/tejesti/reguliruemi-dambeli-bodyflex-2x15kg-i-lost-stanga%20_4_-74x74.webp" },
                    },
                    Reviews = new List<Review>
                    {
                        new Review
                        {
                            UserId = users.First().Id,
                            Content = "Отличен продукт!",
                            Rating = 5
                        }
                    }
                },
                new()
                {
                    Title = "Комбиниран уред MASTER Poseidon",
                    Description = "",
                    MainImageUrl = "https://sportensklad.bg/image/cache/webp/catalog/FITNES_UREDI/kombinirani-uredi/MAS-HG1104-kombiniran-ured-master-poseidon-272x270.webp",
                    RegularPrice = 1883.24m,
                    DiscountPercentage = 0,
                    DiscountedPrice = 0m,
                    Quantity = 25,
                    Rating = 5,
                    CategoryId = categories.First(c => c.Name == "Фитнес").Id,
                    SecondaryImages = new List<Image>
                    {
                        new Image { Uri = "https://sportensklad.bg/image/cache/webp/catalog/mastersport/mas-hg1104MAS-HG1104_22-74x74.webp" },
                        new Image { Uri = "https://sportensklad.bg/image/cache/webp/catalog/mastersport/mas-hg1104MAS-HG1104_22b-74x74.webp" },
                        new Image { Uri = "https://sportensklad.bg/image/cache/webp/catalog/mastersport/mas-hg1104MAS-HG1104_22c-74x74.webp" },
                        new Image { Uri = "https://sportensklad.bg/image/cache/webp/catalog/mastersport/mas-hg1104MAS-HG1104_22d-74x74.webp" },
                        new Image { Uri = "https://sportensklad.bg/image/cache/webp/catalog/mastersport/mas-hg1104MAS-HG1104_22d_2-74x74.webp" },
                    },
                    Reviews = new List<Review>
                    {
                        new Review
                        {
                            UserId = users.First().Id,
                            Content = "Отличен продукт!",
                            Rating = 5
                        }
                    }
                },
                new()
                {
                    Title = "Бягаща пътека Bodyflex Run 1200, електрическа",
                    Description = "",
                    MainImageUrl = "https://sportensklad.bg/image/cache/webp/catalog/FITNES_UREDI/biagashti-pateki/biagasta-pateka-bodyflex-run-1200--BF-39004-272x270.webp",
                    RegularPrice = 790m,
                    DiscountPercentage = 39,
                    DiscountedPrice = 478m,
                    Quantity = 125,
                    Rating = 4,
                    CategoryId = categories.First(c => c.Name == "Фитнес").Id,
                    SecondaryImages = new List<Image>
                    {
                        new Image { Uri = "https://sportensklad.bg/image/cache/webp/catalog/FITNES_UREDI/biagashti-pateki/biagasta-pateka-bodyflex-run-1200--BF-39004--3-74x74.webp" },
                        new Image { Uri = "https://sportensklad.bg/image/cache/webp/catalog/FITNES_UREDI/biagashti-pateki/biagasta-pateka-bodyflex-run-1200--BF-39004--6-74x74.webp" },
                        new Image { Uri = "https://sportensklad.bg/image/cache/webp/catalog/FITNES_UREDI/biagashti-pateki/byagashta-pateka-bodyflex-run-1200-elektricheska-13-74x74.webp" },
                        new Image { Uri = "https://sportensklad.bg/image/cache/webp/catalog/FITNES_UREDI/biagashti-pateki/biagasta-pateka-bodyflex-run-1200--BF-39004-74x74.webp" },
                        new Image { Uri = "https://sportensklad.bg/image/cache/webp/catalog/FITNES_UREDI/biagashti-pateki/byagashta-pateka-bodyflex-run-1200-elektricheska-9-74x74.webp" },
                    },
                    Reviews = new List<Review>
                    {
                        new Review
                        {
                            UserId = users.First().Id,
                            Content = "Много добър продукт.",
                            Rating = 4
                        }
                    }
                },
                new()
                {
                    Title = "Мрежа MASTER Kombi, с регулируема височина",
                    Description = "",
                    MainImageUrl = "https://sportensklad.bg/image/cache/webp/catalog/mastersport/mas-b065MAS-B065-272x270.webp",
                    RegularPrice = 156.40m,
                    DiscountPercentage = 12,
                    DiscountedPrice = 137.63m,
                    Quantity = 15,
                    Rating = 5,
                    CategoryId = categories.First(c => c.Name == "Тенис").Id,
                    SecondaryImages = new List<Image>
                    {
                        new Image { Uri = "https://sportensklad.bg/image/cache/webp/catalog/mastersport/mas-b065MAS-B065a-74x74.webp" },
                        new Image { Uri = "https://sportensklad.bg/image/cache/webp/catalog/mastersport/mas-b065MAS-B065_new-74x74.webp" },
                        new Image { Uri = "https://sportensklad.bg/image/cache/webp/catalog/mastersport/mas-b065MAS-B065c-74x74.webp" },
                    },
                    Reviews = new List<Review>
                    {
                        new Review
                        {
                            UserId = users.First().Id,
                            Content = "Отличен продукт!",
                            Rating = 5
                        }
                    }
                },
                new()
                {
                    Title = "Тенис грип Spartan Soft 60",
                    Description = "",
                    MainImageUrl = "https://sportensklad.bg/image/cache/webp/catalog/SPORTNI_STOKI/tenis-na-kort/tenis-grip-spartan-soft-60-272x270.webp",
                    RegularPrice = 111.78m,
                    DiscountPercentage = 0,
                    DiscountedPrice = 0m,
                    Quantity = 40,
                    Rating = 5,
                    CategoryId = categories.First(c => c.Name == "Тенис").Id,
                    Reviews = new List<Review>
                    {
                        new Review
                        {
                            UserId = users.First().Id,
                            Content = "Отличен продукт!",
                            Rating = 5
                        }
                    }
                },
                new()
                {
                    Title = "Топки за скуош SPARTAN - двойно жълта точка",
                    Description = "",
                    MainImageUrl = "https://sportensklad.bg/image/cache/webp/catalog/SPORTNI_STOKI/tenis-na-kort/topki-za-skuosh-S2448-272x270.webp",
                    RegularPrice = 5.52m,
                    DiscountPercentage = 11,
                    DiscountedPrice = 4.86m,
                    Quantity = 20,
                    Rating = 5,
                    CategoryId = categories.First(c => c.Name == "Тенис").Id,
                    Reviews = new List<Review>
                    {
                        new Review
                        {
                            UserId = users.First().Id,
                            Content = "Отличен продукт!",
                            Rating = 5
                        }
                    }
                },
                new()
                {
                    Title = "Футболна топка SPARTAN Club Junior 3",
                    Description = "",
                    MainImageUrl = "https://sportensklad.bg/image/cache/webp/catalog/SPORTNI_STOKI/futbol1/topki/futbolna-topka-spartan-s42-272x270.webp",
                    RegularPrice = 47.38m,
                    DiscountPercentage = 12,
                    DiscountedPrice = 41.69m,
                    Quantity = 20,
                    Rating = 5,
                    CategoryId = categories.First(c => c.Name == "Футбол").Id,
                    Reviews = new List<Review>
                    {
                        new Review
                        {
                            UserId = users.First().Id,
                            Content = "Отличен продукт!",
                            Rating = 5
                        }
                    }
                },
                new()
                {
                    Title = "Футболна врата SPARTAN Quick Set Up 270 x 150 cm",
                    Description = "",
                    MainImageUrl = "https://sportensklad.bg/image/cache/webp/catalog/mastersport/s1137S1137_23-272x270.webp",
                    RegularPrice = 145.36m,
                    DiscountPercentage = 0,
                    DiscountedPrice = 0m,
                    Quantity = 12,
                    Rating = 0,
                    CategoryId = categories.First(c => c.Name == "Футбол").Id,
                    SecondaryImages = new List<Image>
                    {
                        new Image { Uri = "https://sportensklad.bg/image/cache/webp/catalog/mastersport/s1137S1137_23a-74x74.webp" },
                    },
                    Reviews = new List<Review>
                    {
                        new Review
                        {
                            UserId = users.First().Id,
                            Content = "Пълна нула!",
                            Rating = 0
                        }
                    }
                },
                new()
                {
                    Title = "Футболна врата MASTER Д: 182 x В: 122 x Ш: 61см",
                    Description = "",
                    MainImageUrl = "https://sportensklad.bg/image/cache/webp/catalog/master-pic-upload/futbolna-vrata-MASSPSO-0006-272x270.webp",
                    RegularPrice = 79.58m,
                    DiscountPercentage = 0,
                    DiscountedPrice = 0m,
                    Quantity = 123,
                    Rating = 5,
                    CategoryId = categories.First(c => c.Name == "Футбол").Id,
                    SecondaryImages = new List<Image>
                    {
                        new Image { Uri = "https://sportensklad.bg/image/cache/webp/catalog/mastersport/masspso-0006MASSPSO-0006-74x74.webp" },
                        new Image { Uri = "https://sportensklad.bg/image/cache/webp/catalog/mastersport/masspso-0006MASSPSO-0006a-74x74.webp" },
                    },
                    Reviews = new List<Review>
                    {
                        new Review
                        {
                            UserId = users.First().Id,
                            Content = "Отличен продукт!",
                            Rating = 5
                        }
                    }
                },
                new()
                {
                    Title = "Баскетболна топка SPALDING Silver Series,",
                    Description = "",
                    MainImageUrl = "https://sportensklad.bg/image/cache/webp/catalog/SPORTNI_STOKI/basketbol/basketbolna-topka-spalding-silver-series-razmer-7-272x270.webp",
                    RegularPrice = 82.86m,
                    DiscountPercentage = 11,
                    DiscountedPrice = 72.86m,
                    Quantity = 12,
                    Rating = 3,
                    CategoryId = categories.First(c => c.Name == "Баскетбол").Id,
                    Reviews = new List<Review>
                    {
                        new Review
                        {
                            UserId = users.First().Id,
                            Content = "Добър продукт.",
                            Rating = 3
                        }
                    }
                },
                new()
                {
                    Title = "Баскетболен ринг MASTER 45 см с мрежа",
                    Description = "",
                    MainImageUrl = "https://sportensklad.bg/image/cache/webp/catalog/SPORTNI_STOKI/basketbol/MASSPSB-01-basketball-ring-16-mm-with-net-1-272x270.webp",
                    RegularPrice = 39.56m,
                    DiscountPercentage = 0,
                    DiscountedPrice = 0m,
                    Quantity = 13,
                    Rating = 4,
                    CategoryId = categories.First(c => c.Name == "Баскетбол").Id,
                    SecondaryImages = new List<Image>
                    {
                        new Image { Uri = "https://sportensklad.bg/image/cache/webp/catalog/mastersport/masspsb-01MASSPSB-01-20c-74x74.webp" },
                        new Image { Uri = "https://sportensklad.bg/image/cache/webp/catalog/mastersport/masspsb-01MASSPSB-01-20a-74x74.webp" },
                        new Image { Uri = "https://sportensklad.bg/image/cache/webp/catalog/mastersport/masspsb-01MASSPSB-01-20b-74x74.webp" },
                        new Image { Uri = "https://sportensklad.bg/image/cache/webp/catalog/mastersport/masspsb-01MASSPSB-01_newc-74x74.webp" }
                    }
                },
                new()
                {
                    Title = "Баскетболна топка SPALDING Varsity TF150",
                    Description = "",
                    MainImageUrl = "https://sportensklad.bg/image/cache/webp/catalog/mastersport/spg84325zSPG84325Z-272x270.webp",
                    RegularPrice = 65.32m,
                    DiscountPercentage = 0,
                    DiscountedPrice = 0m,
                    Quantity = 22,
                    Rating = 3,
                    CategoryId = categories.First(c => c.Name == "Баскетбол").Id,
                    Reviews = new List<Review>
                    {
                        new Review
                        {
                            UserId = users.First().Id,
                            Content = "Добър продукт.",
                            Rating = 3
                        }
                    }
                },
                new()
                {
                    Title = "Предпазна каска MASTER Flip, S,",
                    Description = "",
                    MainImageUrl = "https://sportensklad.bg/image/cache/webp/catalog/master-pic-upload/kaska-mas-b200-yellow-272x270.webp",
                    RegularPrice = 22.54m,
                    DiscountPercentage = 11,
                    DiscountedPrice = 0m,
                    Quantity = 13,
                    Rating = 2,
                    CategoryId = categories.First(c => c.Name == "Колоездене").Id,
                    SecondaryImages = new List<Image>
                    {
                        new Image { Uri = "https://sportensklad.bg/image/cache/webp/catalog/mastersport/mas-b200-s-yellowMAS-B200-yellowa-74x74.webp" },
                        new Image { Uri = "https://sportensklad.bg/image/cache/webp/catalog/mastersport/mas-b200-s-yellowMAS-B200-yellowb-74x74.webp" },
                        new Image { Uri = "https://sportensklad.bg/image/cache/webp/catalog/mastersport/mas-b200-s-yellowMAS-B200-yellowc-74x74.webp" }
                    },
                    Reviews = new List<Review>
                    {
                        new Review
                        {
                            UserId = users.First().Id,
                            Content = "Среден продукт.",
                            Rating = 2
                        }
                    }
                },
                new()
                {
                    Title = "Протектори MASTER Kinder, L, комплект",
                    Description = "",
                    MainImageUrl = "https://sportensklad.bg/image/cache/webp/catalog/mastersport/mas-b022-lMAS-B022_2017-272x270.webp",
                    RegularPrice = 17.94m,
                    DiscountPercentage = 0,
                    DiscountedPrice = 0m,
                    Quantity = 5,
                    Rating = 4,
                    CategoryId = categories.First(c => c.Name == "Колоездене").Id,
                    Reviews = new List<Review>
                    {
                        new Review
                        {
                            UserId = users.First().Id,
                            Content = "Много добър продукт.",
                            Rating = 4
                        }
                    }
                },
                new()
                {
                    Title = "Велосипед Sprint Monza Team 28'', 550мм,",
                    Description = "",
                    MainImageUrl = "https://sportensklad.bg/image/cache/webp/catalog/shockblaze./75a9dd3eb836018f91626eadf675b3e5-272x270.webp",
                    RegularPrice = 1752m,
                    DiscountPercentage = 0,
                    DiscountedPrice = 0m,
                    Quantity = 2,
                    Rating = 5,
                    CategoryId = categories.First(c => c.Name == "Колоездене").Id,
                    Reviews = new List<Review>
                    {
                        new Review
                        {
                            UserId = users.First().Id,
                            Content = "Отличен продукт!",
                            Rating = 5
                        }
                    }
                },
                new()
                {
                    Title = "Боксов тренажор Bodyflex, 160cm, надуваем",
                    Description = "",
                    MainImageUrl = "https://sportensklad.bg/image/cache/webp/catalog/SPORTNI_STOKI/boks/BF-046-boksov-trenazhor-bodyflex-160cm-naduvaem-2-272x270.webp",
                    RegularPrice = 46.90m,
                    DiscountPercentage = 0,
                    DiscountedPrice = 0m,
                    Quantity = 18,
                    Rating = 5,
                    CategoryId = categories.First(c => c.Name == "Бокс").Id,
                    SecondaryImages = new List<Image>
                    {
                        new Image { Uri = "https://sportensklad.bg/image/cache/webp/catalog/SPORTNI_STOKI/boks/BF-046-boksov-trenazhor-bodyflex-160cm-naduvaem--74x74.webp" },
                        new Image { Uri = "https://sportensklad.bg/image/cache/webp/catalog/SPORTNI_STOKI/boks/BF-046-boksov-trenazhor-bodyflex-160cm-naduvaem-2-74x74.webp" },
                        new Image { Uri = "https://sportensklad.bg/image/cache/webp/catalog/SPORTNI_STOKI/boks/BF-046-boksov-trenazhor-bodyflex-160cm-naduvaem-1-74x74.webp" },
                        new Image { Uri = "https://sportensklad.bg/image/cache/webp/catalog/SPORTNI_STOKI/boks/BF-046-boksov-trenazhor-bodyflex-160cm-naduvaem-4-74x74.webp" }
                    },
                    Reviews = new List<Review>
                    {
                        new Review
                        {
                            UserId = users.First().Id,
                            Content = "Отличен продукт!",
                            Rating = 5
                        }
                    }
                },
                new()
                {
                    Title = "Боксови ръкавици MASTER TG12",
                    Description = "",
                    MainImageUrl = "https://sportensklad.bg/image/cache/webp/catalog/master-pic-upload/boksovi-rakavici-master-mas-db012-272x270.webp",
                    RegularPrice = 43.70m,
                    DiscountPercentage = 0,
                    DiscountedPrice = 0m,
                    Quantity = 17,
                    Rating = 4,
                    CategoryId = categories.First(c => c.Name == "Бокс").Id,
                    SecondaryImages = new List<Image>
                    {
                        new Image { Uri = "https://sportensklad.bg/image/cache/webp/catalog/mastersport/mas-db012MAS-DB008a-74x74.webp" },
                        new Image { Uri = "https://sportensklad.bg/image/cache/webp/catalog/mastersport/mas-db012MAS-DB010_new-74x74.webp" },
                        new Image { Uri = "https://sportensklad.bg/image/cache/webp/catalog/mastersport/mas-db012MAS-DB010_newa-74x74.webp" },
                        new Image { Uri = "https://sportensklad.bg/image/cache/webp/catalog/mastersport/mas-db012MAS-DB010_newb-74x74.webp" }
                    },
                    Reviews = new List<Review>
                    {
                        new Review
                        {
                            UserId = users.First().Id,
                            Content = "Много добър продукт.",
                            Rating = 4
                        }
                    }
                },
                new()
                {
                    Title = "Боксов чувал SPARTAN 90 cм, 20 кг",
                    Description = "",
                    MainImageUrl = "https://sportensklad.bg/image/cache/webp/catalog/mastersport/s1194S1194-272x270.webp",
                    RegularPrice = 261.28m,
                    DiscountPercentage = 0,
                    DiscountedPrice = 0m,
                    Quantity = 7,
                    Rating = 4,
                    CategoryId = categories.First(c => c.Name == "Бокс").Id,
                    SecondaryImages = new List<Image>
                    {
                        new Image { Uri = "https://sportensklad.bg/image/cache/webp/catalog/SPORTNI_STOKI/boks/boksov-chuval-spartan-90-sm-20-kg-74x74.webp" }
                    },
                    Reviews = new List<Review>
                    {
                        new Review
                        {
                            UserId = users.First().Id,
                            Content = "Много добър продукт.",
                            Rating = 4
                        }
                    }
                },
            ];

            db.Products.AddRange(products);
            await db.SaveChangesAsync();
        }
    }
}
