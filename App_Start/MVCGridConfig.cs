[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(DBESearchAdmin.MVCGridConfig), "RegisterGrids")]

namespace DBESearchAdmin
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Linq;
    using System.Collections.Generic;
    using DBESearchAdmin.Models;

    using MVCGrid.Models;
    using MVCGrid.Web;

    public static class MVCGridConfig 
    {
        public static void RegisterGrids()
        {

            MVCGridDefinitionTable.Add("UsageExample", new MVCGridBuilder<DBESearchAdmin.Models.DBECompany>()
             .WithPaging(paging: true, itemsPerPage: 10, allowChangePageSize: true, maxItemsPerPage: 100)
  
     
                .WithRetrieveDataMethod((context) =>
                {
                    return new QueryResult<YourModelItem>()
                    {
                        Items = new List<YourModelItem>(),
                        TotalRecords = 0 // if paging is enabled, return the total number of records of all pages
                    };

                })
            );

        }
    }
}