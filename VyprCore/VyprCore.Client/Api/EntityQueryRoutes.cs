// <copyright file="{MANY}.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Client.Api
{
    using VyprCore.Interfaces.Client;

    public class QueryByUser : IQueryRoute
    {
        public string QueryRoute => "byuser";
    }

    public class QueryByRole : IQueryRoute
    {
        public string QueryRoute => "byrole";
    }

    public class QueryByTenant : IQueryRoute
    {
        public string QueryRoute => "bytenant";
    }
}
