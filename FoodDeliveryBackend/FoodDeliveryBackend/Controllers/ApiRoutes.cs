namespace FoodDeliveryBackend.Controllers
{
    public static class ApiRoutes
    {
        public static class Restoraunts
        {
            public const string Base = "api/restaurants";
            public const string GetAll = $"{Base}/all";
            public const string GetById = $"{Base}/{{id}}";
            public const string Create = $"{Base}";
            public const string Update = $"{Base}/{{id}}";
            public const string Delete = $"{Base}/{{id}}";
        }

        public static class Dishes
        {
            public const string Base = "api/dishes";
            public const string GetAll = $"{Base}/all";
            public const string GetById = $"{Base}/{{id}}";
            public const string Create = $"{Base}";
            public const string Update = $"{Base}/{{id}}";
            public const string Delete = $"{Base}/{{id}}";
        }

        public static class Menues
        {
            public const string Base = "api/menus";
            public const string GetAll = $"{Base}/all";
            public const string GetById = $"{Base}/{{id}}";
            public const string Create = $"{Base}";
            public const string Update = $"{Base}/{{id}}";
            public const string Delete = $"{Base}/{{id}}";
        }
    }
}
