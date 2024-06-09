using Application.Authors.Models;
using Application.Books.Model;
using Application.Books.Requests;
using Domain.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public static class MapsterConfig
    {
        public static void RegisterMappings()
        {

            TypeAdapterConfig<BookModel, UpdateBookRequest>
                .NewConfig()
                .Map(x => x.AuthorIds, x => x.Authors.Select(x => x.Id));
        }
    }
}
