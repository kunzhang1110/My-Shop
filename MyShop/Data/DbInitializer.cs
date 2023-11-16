using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyShop.Entities;

namespace MyShop.Data
{
    /// <summary>
    /// Initializes database with default users and products.
    /// </summary>
    public static class DbInitializer
    {
        public static async Task Initialize(MyShopContext context, UserManager<User> userManager)
        {

            if (!userManager.Users.Any())
            {
                var user = new User
                {
                    UserName = "a",
                    Email = "a@a.com"
                };

                await userManager.CreateAsync(user, "Zk000000!");
                await userManager.AddToRoleAsync(user, "Member");

                var admin = new User
                {
                    UserName = "admin",
                    Email = "admin@a.com"
                };

                await userManager.CreateAsync(admin, "Zk000000!");
                await userManager.AddToRolesAsync(admin, new string[] { "Member", "Admin" });

            }


            if (context.Products.Any()) return;

            var products = new List<Product>
            {
                 new Product
                {
                    Name = "SCM Black Clip On Earrings",
                    Description = "Black Stainless Steel (contains Titanium) clip on earrings, 4mm wide.",
                    Price =  300,
                    PictureUrl = "/images/products/apparels-scm-black-clip-on-earrings.jpg",
                    Brand = "SCM",
                    Type = "Apparels",
                    QuantityInStock =  1
                },
                new Product
                {
                    Name = "SCM Black Barbell Earrings",
                    Description = "Black Stainless Steel (contains Titanium) stud earrings, 4mm wide.",
                    Price =  500,
                    PictureUrl = "/images/products/apparels-scm-black-barbell-earrings.jpg",
                    Brand = "SCM",
                    Type = "Apparels",
                    QuantityInStock =  2
                },
                new Product
                {
                    Name = "SCM Black Cable Chain",
                    Description = "Black Stainless Steel, 50cm long.",
                    Price =  400,
                    PictureUrl = "/images/products/apparels-scm-black-cable-chain.jpg",
                    Brand = "SCM",
                    Type = "Apparels",
                    QuantityInStock =  1
                },
                new Product
                {
                    Name = "Google Chromecast 4k",
                    Description = "The Chromecast with Google TV (4K) brings you the entertainment that you love, in up to 4K HDR. Get personal recommendations based on your subscriptions and streaming history all in one place. No more jumping between apps to decide what to watch. And use the remote to search for movies and shows with your voice.",
                    Price =  9800,
                    PictureUrl = "/images/products/electronics-google-chromecast-4k.jpg",
                    Brand = "Google",
                    Type = "Electronics",
                    QuantityInStock =  12
                },
                new Product
                {
                    Name = "Breville Duo Temp Pro Coffee Machine",
                    Description = "This Breville The Duo Temp Pro Coffee Machine has a compact design that makes it great for adding to the bench in your home or workplace kitchen, providing a spot to make a delicious cup of coffee whenever you need one. The machine uses low pressure pre-infusion to help draw out flavours evenly, resulting in a smooth coffee. Plus, there's a milk jug included so you'll be able to add frothed milk to your drinks.",
                    Price =  33900,
                    PictureUrl = "/images/products/electronics-breville-duo-temp-pro-coffee-machine.jpg",
                    Brand = "Breville",
                    Type = "Electronics",
                    QuantityInStock =  10
                },
                new Product
                {
                    Name = "Breville BKE495 Electric Kettle",
                    Description = "This Breville BKE495 Electric Kettle features a stainless steel finish that is both elegant and corrosion resistant. It has a capacity of 1.7l with a clear volume indicator designed to emphasise the current water level. It also has two switches, one on the top for the lid and one near the base to control the power. The power base allows for 360-degree access while also doubling as a cord storage area.",
                    Price =  9900,
                    PictureUrl = "/images/products/electronics-breville-bke495-electric-kettle.jpg",
                    Brand = "Breville",
                    Type = "Electronics",
                    QuantityInStock =  2
                },
                new Product
                {
                    Name = "Google Nest Mini 2nd Generation Smart Speaker",
                    Description = "Meet the 2nd generation Nest Mini, the speaker you control with your voice. Just say \"Hey Google\" to play your favourite music from Spotify, YouTube Music, and more.",
                    Price =  6500,
                    PictureUrl = "/images/products/electronics-google-nest-mini-2nd-generation-smart-speaker.jpg",
                    Brand = "Google",
                    Type = "Electronics",
                    QuantityInStock =  3
                },
                new Product
                {
                    Name = "Microsoft Surface Go Type Cover",
                    Description = "Sleek, compact and adjustable, Surface Go Type Cover performs like a traditional, full-size keyboard. Plus, three colours are covered in rich, warm Alcantara material for an added touch of luxury.",
                    Price =  20000,
                    PictureUrl = "/images/products/electronics-microsoft-surface-go-type-cover.jpg",
                    Brand = "Microsoft",
                    Type = "Electronics",
                    QuantityInStock =  5
                },
                new Product
                {
                    Name = "Microsoft 3500 Wireless Mobile Mouse",
                    Description = "This compact mouse works on virtually any surface* thanks to Microsoft BlueTrack Technology, which combines the power of optical with the precision of laser. Keep its nano transceiver plugged into your PC, or stow it in the mouse for safe keeping.",
                    Price =  2900,
                    PictureUrl = "/images/products/electronics-microsoft-3500-wireless-mobile-mouse.jpg",
                    Brand = "Microsoft",
                    Type = "Electronics",
                    QuantityInStock =  2
                },
                new Product
                {
                    Name = "Nintendo Switch Pokemon Let’s Go Pikachu Poke Ball Plus Set",
                    Description = "The Poké Ball Plus is a Poké Ball-shaped device that can be used to play Pokémon: Let’s Go, Pikachu! and Pokémon: Let’s Go, Eevee! in place of your Joy-Con.",
                    Price =  15000,
                    PictureUrl = "/images/products/games-nintendo-switch-pokemon-let’s-go-pikachu-poke-ball-plus-set.jpg",
                    Brand = "Nintendo Switch",
                    Type = "Games",
                    QuantityInStock =  1
                },
                new Product
                {
                    Name = "Nintendo Switch Mario Kart 8 Deluxe",
                    Description = "Hit the road with the definitive version of Mario Kart 8 and play anytime, anywhere! Race your friends or battle them in a revised battle mode on new and returning battle courses. Play locally in up to 4-player multiplayer in 1080p while playing in TV Mode. Every track from the Wii U version, including DLC, makes a glorious return. ",
                    Price =  5000,
                    PictureUrl = "/images/products/games-nintendo-switch-mario-kart-8-deluxe.jpg",
                    Brand = "Nintendo Switch",
                    Type = "Games",
                    QuantityInStock =  2
                },
                new Product
                {
                    Name = "Play Station 4 The Witcher 3 Complete Edition",
                    Description = "The complete Edition contains every piece of downloadable content released for the game, including two massive story expansions: hearts of stone & Blood and Wine.",
                    Price =  4500,
                    PictureUrl = "/images/products/games-play-station-4-the-witcher-3-complete-edition.jpg",
                    Brand = "Play Station",
                    Type = "Games",
                    QuantityInStock =  1
                },
                new Product
                {
                    Name = "Play Station 4 The Last Of Us Remastered",
                    Description = "Winner of over 200 Game of the Year awards, The Last of Us has been rebuilt for the PlayStation 4 system. Now featuring full 1080p, higher-resolution character models, improved shadows and lighting, in addition to several other gameplay improvements.",
                    Price =  4500,
                    PictureUrl = "/images/products/games-play-station-4-the-last-of-us-remastered.jpg",
                    Brand = "Play Station",
                    Type = "Games",
                    QuantityInStock =  1
                },
            };

            foreach (var product in products)
            {
                context.Products.Add(product);
            }

            context.SaveChanges();
        }
    }
}