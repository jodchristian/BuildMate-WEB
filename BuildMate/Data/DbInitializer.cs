using Buildmate.Models;

namespace Buildmate.Data
{
    public static class DbInitializer
    {
        public static void Seed(ApplicationDbContext context)
        {
            if (!context.RfqRequests.Any())
            {
                var rfqs = new List<RfqRequest>();
                int counter = 12;
                for (int i = 0; i < 8; i++)
                {
                    rfqs.Add(new RfqRequest
                    {
                        Reference = $"RFQ-25052{counter}",
                        Title = "Office Building - Electrical & Plumbing Materials",
                        Buyer = "Skyline Constructions",
                        Category = "Electrical, Plumbing",
                        PostedDate = new DateTime(2025, 5, 20),
                        ValidTill = new DateTime(2025, 5, 27),
                        ItemsCount = 4,
                        Status = "New"
                    });
                    counter--;
                }
                for (int i = 0; i < 5; i++)
                {
                    rfqs.Add(new RfqRequest
                    {
                        Reference = $"RFQ-25052{counter}",
                        Title = "Office Building - Electrical & Plumbing Materials",
                        Buyer = "Skyline Constructions",
                        Category = "Electrical, Plumbing",
                        PostedDate = new DateTime(2025, 5, 18),
                        ValidTill = new DateTime(2025, 5, 25),
                        ItemsCount = 4,
                        Status = "Responded"
                    });
                    counter--;
                }
                for (int i = 0; i < 12; i++)
                {
                    rfqs.Add(new RfqRequest
                    {
                        Reference = $"RFQ-25051{counter}",
                        Title = "Warehouse Project - Roofing Materials",
                        Buyer = "Skyline Constructions",
                        Category = "Roofing",
                        PostedDate = new DateTime(2025, 5, 1),
                        ValidTill = new DateTime(2025, 5, 15),
                        ItemsCount = 3,
                        Status = "Closed"
                    });
                    counter--;
                }
                context.RfqRequests.AddRange(rfqs);
            }

            if (!context.Orders.Any())
            {
                var orderStatuses = new List<string>();
                orderStatuses.AddRange(Enumerable.Repeat("To Confirm", 5));
                orderStatuses.AddRange(Enumerable.Repeat("To Ship", 12));
                orderStatuses.AddRange(Enumerable.Repeat("In Transit", 8));
                orderStatuses.AddRange(Enumerable.Repeat("Delivered", 15));
                orderStatuses.AddRange(Enumerable.Repeat("Cancelled", 2));

                var projects = new[] { "Skylane Residences", "Office Tower Manila", "Warehouse Complex", "Riverside Condominiums" };
                var buyers = new[] { "Skyline Constructions", "Meridian Builders", "Apex Development Corp" };

                var orders = new List<Order>();
                var rnd = new Random(42);
                int orderCounter = 17;

                foreach (var status in orderStatuses)
                {
                    var orderDate = new DateTime(2025, 5, 20);
                    var expected = status switch
                    {
                        "Delivered" => orderDate.AddDays(7),
                        "Cancelled" => orderDate.AddDays(5),
                        _ => DateTime.Now.AddDays(rnd.Next(-3, 10))
                    };

                    orders.Add(new Order
                    {
                        OrderNumber = $"ORD-250520-{orderCounter:0000}",
                        ProjectName = projects[rnd.Next(projects.Length)],
                        BuyerName = buyers[rnd.Next(buyers.Length)],
                        OrderDate = orderDate,
                        Amount = 14200.50m,
                        ItemsCount = 4,
                        Status = status,
                        ExpectedDelivery = expected
                    });
                    orderCounter--;
                }
                context.Orders.AddRange(orders);
            }

            if (!context.Quotations.Any())
            {
                var quotations = new List<Quotation>();
                for (int i = 0; i < 5; i++) quotations.Add(new Quotation { Status = "Accepted", SentDate = DateTime.Now.AddDays(-i) });
                for (int i = 0; i < 6; i++) quotations.Add(new Quotation { Status = "Pending", SentDate = DateTime.Now.AddDays(-i) });
                for (int i = 0; i < 4; i++) quotations.Add(new Quotation { Status = "Declined", SentDate = DateTime.Now.AddDays(-i) });
                context.Quotations.AddRange(quotations);
            }

            if (!context.ActivityLogs.Any())
            {
                context.ActivityLogs.AddRange(
                    new ActivityLog { Title = "New RFQ received", Description = "Office Building - Electrical Materials", Timestamp = DateTime.Now.AddHours(-3), IconType = "rfq" },
                    new ActivityLog { Title = "Quotation accepted", Description = "Order ORD-250510-0012 accepted", Timestamp = DateTime.Now.AddHours(-8), IconType = "quotation" },
                    new ActivityLog { Title = "Quotation accepted", Description = "Order ORD-250510-0012 accepted", Timestamp = DateTime.Now.AddHours(-20), IconType = "quotation" },
                    new ActivityLog { Title = "Order shipped", Description = "Order ORD-25051 has been shipped", Timestamp = DateTime.Now.AddDays(-1), IconType = "order" }
                );
            }

            if (!context.Products.Any())
            {
                var categories = new[] { "Cement", "Steel", "Electrical", "Plumbing", "Roofing", "Insulation" };
                var names = new[] { "OPC 53 Grade Cement", "Fiberglass Insulation", "PVC Pipe 4-inch", "Steel Rebar 12mm", "Galvanized Roof Sheet", "Copper Wire 2.5mm", "Ceramic Tiles", "Hollow Blocks" };
                var units = new[] { "Bag (50 kg)", "Roll", "Piece", "Meter", "Sheet", "Box" };
                var rnd2 = new Random(7);

                var products = new List<Product>();
                for (int i = 0; i < 126; i++)
                {
                    var name = names[rnd2.Next(names.Length)];
                    products.Add(new Product
                    {
                        Name = name,
                        Description = $"High quality {name.ToLower()} for all construction needs.",
                        Sku = $"CEM-{53 + (i % 10)}",
                        Category = categories[rnd2.Next(categories.Length)],
                        Unit = units[rnd2.Next(units.Length)],
                        Price = 410.00m,
                        Status = i % 6 == 0 ? "Inactive" : "Active",
                        ImageFile = name.Contains("Fiberglass") ? "fiberclass.png" : "gradecement.png"
                    });
                }
                context.Products.AddRange(products);
            }
            if (!context.CompanyProfiles.Any())
            {
                context.CompanyProfiles.Add(new CompanyProfile
                {
                    CompanyName = "BuildWell Enterprises",
                    LogoFile = "compannyprof.png",
                    IsVerified = true,
                    SinceDate = new DateTime(2024, 3, 1),
                    ShortDescription = "Your trusted partner in providing high-quality construction materials and reliable supply solutions for every build.",
                    Address = "123 Industrial Avenue, Brgy. San Isidro, Quezon City, NCR, Philippines",
                    Phone = "(02) 8123 4567",
                    Email = "info@buildwell.ph",
                    Website = "www.buildwell.com",
                    TotalOrders = 126,
                    AverageRating = 4.8m,
                    ReviewsCount = 128,
                    ResponseRate = 97,
                    AboutCompany = "BuildWell Enterprises is a leading supplier of construction materials committed to quality, reliability, and customer satisfaction. We cater to contractors, builders, and developers of all sizes.",
                    BusinessType = "Construction Materials Supplier",
                    YearEstablished = 2018,
                    CompanySize = "51-200 Employees",
                    MainCategories = "Cement & Concrete,Steel & Metal,Bricks & Blocks",
                    ServiceAreas = "NCR, CALABARZON, Central Luzon",
                    DtiRegNo = "DTI-3829281",
                    BusinessPermitNo = "BP-2024-12394",
                    Tin = "345-012-312-455",
                    VatRegistered = true
                });
            }

            if (!context.CompanyDocuments.Any())
            {
                context.CompanyDocuments.AddRange(
                    new CompanyDocument { FileName = "DTI Registration.pdf", UploadedDate = new DateTime(2024, 5, 15), ColorTag = "#fca5a5" },
                    new CompanyDocument { FileName = "Business Permit.pdf", UploadedDate = new DateTime(2024, 5, 15), ColorTag = "#fde68a" },
                    new CompanyDocument { FileName = "BIR Certificate.pdf", UploadedDate = new DateTime(2024, 5, 15), ColorTag = "#86efac" }
                );
            }
            if (!context.Notifications.Any())
            {
                var notifs = new List<NotificationItem>
    {
        new() { Category = "RFQ & Quotations", IconType = "rfq", Title = "New Quotation Received", Description = "You have received a new quotation for RFQ #RFQ-2505-0017 from Skyline Constructions.", Tag = "RFQ-2505-0017", Timestamp = DateTime.Now.Date.AddHours(10).AddMinutes(24), IsRead = false },
        new() { Category = "Orders & Shipments", IconType = "order", Title = "Order Confirmed", Description = "Order #ORD-2505-0012 from BuildRight Contractors has been confirmed.", Tag = "RFQ-2505-0017", Timestamp = DateTime.Now.Date.AddHours(9).AddMinutes(24), IsRead = false },
        new() { Category = "RFQ & Quotations", IconType = "rfq", Title = "New Quotation Received", Description = "You have received a new quotation for RFQ #RFQ-2505-0017 from Skyline Constructions.", Tag = "RFQ-2505-0017", Timestamp = DateTime.Now.Date.AddHours(10).AddMinutes(24), IsRead = false },
        new() { Category = "System Updates", IconType = "system", Title = "System Update", Description = "We've updated our Terms of Service. Please review the changes.", Tag = null, Timestamp = DateTime.Now.Date.AddDays(-1).AddHours(16).AddMinutes(30), IsRead = true },
        new() { Category = "RFQ & Quotations", IconType = "rfq", Title = "New Quotation Received", Description = "You have received a new quotation for RFQ #RFQ-2505-0017 from Skyline Constructions.", Tag = "RFQ-2505-0017", Timestamp = new DateTime(2025, 5, 18, 10, 24, 0), IsRead = true },
        new() { Category = "Orders & Shipments", IconType = "order", Title = "Order Confirmed", Description = "Order #ORD-2505-0012 from BuildRight Contractors has been confirmed.", Tag = "RFQ-2505-0017", Timestamp = new DateTime(2025, 5, 18, 9, 24, 0), IsRead = true },
    };

                // Dagdagan pa para maabot ang 23 total (matching reference)
                for (int i = 0; i < 17; i++)
                {
                    var cat = i % 4 == 0 ? "Payments & Invoices" : (i % 3 == 0 ? "System Updates" : (i % 2 == 0 ? "Orders & Shipments" : "RFQ & Quotations"));
                    var icon = cat switch { "Orders & Shipments" => "order", "System Updates" => "system", "Payments & Invoices" => "payment", _ => "rfq" };
                    notifs.Add(new NotificationItem
                    {
                        Category = cat,
                        IconType = icon,
                        Title = cat == "Payments & Invoices" ? "Payment Received" : (cat == "System Updates" ? "System Update" : (cat == "Orders & Shipments" ? "Order Confirmed" : "New Quotation Received")),
                        Description = "Additional notification details will appear here.",
                        Tag = cat == "RFQ & Quotations" ? "RFQ-2505-0017" : null,
                        Timestamp = new DateTime(2025, 5, 17).AddDays(-i).AddHours(8 + (i % 5)),
                        IsRead = true
                    });
                }

                context.Notifications.AddRange(notifs);
            }
            if (!context.UserSettings.Any())
            {
                context.UserSettings.Add(new UserSettings
                {
                    FirstName = "BuildWell",
                    LastName = "Enterprises",
                    Email = "info@buildwell.ph",
                    PhoneNumber = "(02) 8123 4567",
                    LanguagePreference = "English",
                    TimeZone = "GMT+8 (Manila)",
                    TwoFactorEnabled = true,
                    AuthenticatorApp = "Google Authenticator",
                    UnusedBackupCodes = 5
                });
            }

            if (!context.BankAccounts.Any())
            {
                context.BankAccounts.AddRange(
                    new BankAccount { BankName = "BDO Unibank, Inc.", AccountName = "BuildWell Enterprises", AccountNumber = "0123 1231 1231", AccountType = "Savings Account", Status = "Verified", IsPrimary = true },
                    new BankAccount { BankName = "Metrobank", AccountName = "BuildWell Enterprises", AccountNumber = "123 12314 1231 123", Status = "Verified", IsPrimary = false },
                    new BankAccount { BankName = "BPI", AccountName = "BuildWell Enterprises", AccountNumber = "123 12314 1231 123", Status = "Verified", IsPrimary = false },
                    new BankAccount { BankName = "BPI", AccountName = "BuildWell Enterprises", AccountNumber = "123 12314 1231 123", Status = "Verified", IsPrimary = false }
                );
            }

            if (!context.MessageThreads.Any())
            {
                var threads = new List<MessageThread>
    {
        new() { SenderName = "MAKJJ Store", SenderInitials = "MJ", AvatarColor = "#fed7aa", IsOnline = true, RelatedOrder = "Order ORD-2025-1021", ProjectName = "Skyline Tower Phase 2", OrderStatus = "In Transit", LastMessage = "Thank you. Kindly provide jumbo hotdog i want coke.", LastMessageTime = DateTime.Now.Date.AddHours(9).AddMinutes(18), IsUnread = true, Category = "Suppliers" },
        new() { SenderName = "Skyline Constructions", SenderInitials = "SC", AvatarColor = "#bfdbfe", IsOnline = true, RelatedOrder = "RFQ-2505-0017", ProjectName = "Office Building - Electrical", OrderStatus = "Pending", LastMessage = "Can you confirm the delivery date for the cement order?", LastMessageTime = DateTime.Now.Date.AddHours(8).AddMinutes(40), IsUnread = true, Category = "Projects" },
        new() { SenderName = "BuildRight Contractors", SenderInitials = "BR", AvatarColor = "#bbf7d0", IsOnline = false, RelatedOrder = "Order ORD-2505-0012", ProjectName = "Warehouse Complex", OrderStatus = "Delivered", LastMessage = "Thank you, order has been received in good condition.", LastMessageTime = DateTime.Now.Date.AddDays(-1).AddHours(16).AddMinutes(20), IsUnread = false, Category = "Projects" },
        new() { SenderName = "Meridian Builders", SenderInitials = "MB", AvatarColor = "#ddd6fe", IsOnline = false, RelatedOrder = "RFQ-2505-0011", ProjectName = "Riverside Condominiums", OrderStatus = "To Confirm", LastMessage = "We'd like to request a quotation for steel rebar.", LastMessageTime = DateTime.Now.Date.AddDays(-1).AddHours(11).AddMinutes(5), IsUnread = false, Category = "Suppliers" },
        new() { SenderName = "Apex Development Corp", SenderInitials = "AD", AvatarColor = "#fecaca", IsOnline = true, RelatedOrder = "Order ORD-2505-0009", ProjectName = "Office Tower Manila", OrderStatus = "Delivered", LastMessage = "Payment has been processed on our end.", LastMessageTime = DateTime.Now.Date.AddDays(-2).AddHours(14).AddMinutes(30), IsUnread = false, Category = "Suppliers" },
        new() { SenderName = "Golden Hardware Supply", SenderInitials = "GH", AvatarColor = "#fde68a", IsOnline = true, RelatedOrder = "Order ORD-2505-0015", ProjectName = "Skylane Residences", OrderStatus = "To Ship", LastMessage = "Your order is being packed and will ship tomorrow.", LastMessageTime = DateTime.Now.Date.AddDays(-1).AddHours(10).AddMinutes(12), IsUnread = true, Category = "Suppliers" },
        new() { SenderName = "Prime Contractors Inc.", SenderInitials = "PC", AvatarColor = "#a7f3d0", IsOnline = false, RelatedOrder = "RFQ-2505-0009", ProjectName = "Riverside Condominiums", OrderStatus = "Responded", LastMessage = "We've submitted our quotation for the roofing materials.", LastMessageTime = DateTime.Now.Date.AddDays(-2).AddHours(9).AddMinutes(45), IsUnread = false, Category = "Projects" },
        new() { SenderName = "Northstar Builders", SenderInitials = "NB", AvatarColor = "#c7d2fe", IsOnline = true, RelatedOrder = "Order ORD-2505-0006", ProjectName = "Warehouse Complex", OrderStatus = "In Transit", LastMessage = "Truck is on the way, ETA is around 3 PM today.", LastMessageTime = DateTime.Now.Date.AddDays(-2).AddHours(13).AddMinutes(20), IsUnread = false, Category = "Projects" },
        new() { SenderName = "Iron Gate Steelworks", SenderInitials = "IG", AvatarColor = "#fecaca", IsOnline = false, RelatedOrder = "Order ORD-2505-0003", ProjectName = "Office Tower Manila", OrderStatus = "Delivered", LastMessage = "Steel rebar delivery confirmed and signed off.", LastMessageTime = DateTime.Now.Date.AddDays(-3).AddHours(15).AddMinutes(0), IsUnread = false, Category = "Suppliers" },
        new() { SenderName = "Coastal Cement Traders", SenderInitials = "CC", AvatarColor = "#fbcfe8", IsOnline = true, RelatedOrder = "RFQ-2505-0005", ProjectName = "Skylane Residences", OrderStatus = "New", LastMessage = "Sending over our latest cement pricing for review.", LastMessageTime = DateTime.Now.Date.AddDays(-3).AddHours(11).AddMinutes(30), IsUnread = true, Category = "Suppliers" },
        new() { SenderName = "Vertex Development Group", SenderInitials = "VD", AvatarColor = "#bae6fd", IsOnline = false, RelatedOrder = "Order ORD-2505-0002", ProjectName = "Office Building - Electrical", OrderStatus = "To Confirm", LastMessage = "Please confirm the quantity before we proceed.", LastMessageTime = DateTime.Now.Date.AddDays(-4).AddHours(8).AddMinutes(15), IsUnread = false, Category = "Projects" },
        new() { SenderName = "Summit Roofing Co.", SenderInitials = "SR", AvatarColor = "#fed7aa", IsOnline = true, RelatedOrder = "Order ORD-2505-0001", ProjectName = "Warehouse Complex", OrderStatus = "Delivered", LastMessage = "All roofing sheets have been delivered on site.", LastMessageTime = DateTime.Now.Date.AddDays(-5).AddHours(14).AddMinutes(45), IsUnread = false, Category = "Suppliers" }
    };
                context.MessageThreads.AddRange(threads);
                context.SaveChanges(); // save first so we get generated Ids

                context.ChatMessages.AddRange(
                    new ChatMessage { ThreadId = threads[0].Id, Content = "Good morning! Your order has been confirmed.", IsFromMe = false, Timestamp = DateTime.Now.Date.AddHours(9).AddMinutes(15) },
                    new ChatMessage { ThreadId = threads[0].Id, Content = "Thank you. Kindly provide jumbo hotdog i want coke.", IsFromMe = true, Timestamp = DateTime.Now.Date.AddHours(9).AddMinutes(18) },

                    new ChatMessage { ThreadId = threads[1].Id, Content = "Hi! We've reviewed your RFQ submission.", IsFromMe = false, Timestamp = DateTime.Now.Date.AddHours(8).AddMinutes(30) },
                    new ChatMessage { ThreadId = threads[1].Id, Content = "Can you confirm the delivery date for the cement order?", IsFromMe = false, Timestamp = DateTime.Now.Date.AddHours(8).AddMinutes(40) },

                    new ChatMessage { ThreadId = threads[2].Id, Content = "Your order has been marked as delivered.", IsFromMe = false, Timestamp = DateTime.Now.Date.AddDays(-1).AddHours(16).AddMinutes(15) },
                    new ChatMessage { ThreadId = threads[2].Id, Content = "Thank you, order has been received in good condition.", IsFromMe = true, Timestamp = DateTime.Now.Date.AddDays(-1).AddHours(16).AddMinutes(20) }
                );
            }
            context.SaveChanges();
        }
    }
}