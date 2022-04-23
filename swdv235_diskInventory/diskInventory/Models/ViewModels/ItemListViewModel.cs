﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace diskInventory.Models
{
    public class ItemListViewModel
    {
        private InventoryContext context;

        public ItemListViewModel(InventoryContext ctx)
        {
            context = ctx;
        }

        public List<Genre> Genres {
            get
            {
                return context.Genres
                .Include("Items").Include("Items.Status").Include("Items.Type")
                .ToList();
            }
        }

        public string FormatReleaseDate(DateTime date)
        {
            return date.ToString("yyyy/MM/dd");
        }
    }
}
