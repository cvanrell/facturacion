using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.Billing.Entities;
using WIS.Billing.Entities.Enums;

namespace WIS.Billing.DataAccess
{
    public class DataContextDbInitializer : CreateDatabaseIfNotExists<DataContext>
    {
        //protected override void Seed(DataContext context)
        //{
        //    List<Client> clients = new List<Client>();
        //    var client = new Client
        //    {
        //        Id = Guid.NewGuid(),
        //        Description = "Costa Oriental",
        //        RUT = "4444",
        //        Address = "Zona",
        //        HourRates = new List<HourRate> {
        //            new HourRate
        //            {
        //                Id = Guid.NewGuid(),
        //                Description = "Horas estándar",
        //                Currency = Currency.Dollar,
        //                Rate = 50,
        //                AdjustmentPeriodicity = Periodicity.Monthly,
        //                StartingMonth = 1
        //            }
        //        }
        //    };
        //    context.Clients.Add(client);

        //    var maintenance = new Maintenance
        //    {
        //        Id = Guid.NewGuid(),
        //        Description = "Mantenimiento Costa Oriental",
        //        Client = client,
        //        Amount = 2000,
        //        Currency = Currency.Dollar,
        //        Periodicity = Periodicity.Monthly
        //    };
        //    context.Maintenances.Add(maintenance);

        //    client = new Client
        //    {
        //        Id = Guid.NewGuid(),
        //        Description = "Tienda Inglesa",
        //        RUT = "7777",
        //        Address = "18 de julio 1234",
        //        HourRates = new List<HourRate> {
        //            new HourRate
        //            {
        //                Id = Guid.NewGuid(),
        //                Description = "Horas estándar",
        //                Currency = Currency.Dollar,
        //                Rate = 45,
        //                AdjustmentPeriodicity = Periodicity.Quarterly,
        //                StartingMonth = 1
        //            }
        //        }
        //    };
        //    context.Clients.Add(client);

        //    maintenance = new Maintenance
        //    {
        //        Id = Guid.NewGuid(),
        //        Description = "Mantenimiento Tienda Inglesa",
        //        Client = client,
        //        Amount = 4500,
        //        Currency = Currency.Dollar,
        //        Periodicity = Periodicity.Monthly
        //    };
        //    context.Maintenances.Add(maintenance);

        //    var project = new Project
        //    {
        //        Id = Guid.NewGuid(),
        //        Description = "Implementación WIS Locales",
        //        Client = client,
        //        Amount = 199000,
        //        Currency = Currency.Dollar,
        //        Installments = 8,
        //        Fees = new List<Fee>
        //        {
        //            new Fee
        //            {
        //                Id = Guid.NewGuid(),
        //                Description = "Couta inicial",
        //                Month = 1,
        //                Amount = 1000,
        //                IVA = 220,
        //                Total = 2200
        //            },
        //            new Fee
        //            {
        //                Id = Guid.NewGuid(),
        //                Description = "Couta final",
        //                Month = 8,
        //                Amount = 10000,
        //                IVA = 2200,
        //                Total = 12200
        //            }
        //        }
        //    };
        //    context.Projects.Add(project);

        //    client = new Client
        //    {
        //        Id = Guid.NewGuid(),
        //        Description = "TCU",
        //        RUT = "8888",
        //        Address = "Propios 1234",
        //        HourRates = new List<HourRate> {
        //            new HourRate
        //            {
        //                Id = Guid.NewGuid(),
        //                Description = "Horas estándar",
        //                Currency = Currency.Peso,
        //                Rate = 1500,
        //                AdjustmentPeriodicity = Periodicity.Quarterly,
        //                StartingMonth = 1
        //            }
        //        }
        //    };
        //    context.Clients.Add(client);

        //    maintenance = new Maintenance
        //    {
        //        Id = Guid.NewGuid(),
        //        Description = "Mantenimiento TCU",
        //        Client = client,
        //        Amount = 10000,
        //        Currency = Currency.Dollar,
        //        Periodicity = Periodicity.Quarterly
        //    };
        //    context.Maintenances.Add(maintenance);

        //    project = new Project
        //    {
        //        Id = Guid.NewGuid(),
        //        Description = "Tiscar",
        //        Client = client,
        //        Amount = 150000,
        //        Currency = Currency.Dollar,
        //        Installments = 10,
        //        Fees = new List<Fee>
        //        {
        //            new Fee
        //            {
        //                Id = Guid.NewGuid(),
        //                Description = "Couta inicial",
        //                Month = 1,
        //                Amount = 1000,
        //                IVA = 220,
        //                Total = 2200
        //            },
        //            new Fee
        //            {
        //                Id = Guid.NewGuid(),
        //                Description = "Couta final",
        //                Month = 8,
        //                Amount = 10000,
        //                IVA = 2200,
        //                Total = 12200
        //            }
        //        }
        //    };
        //    context.Projects.Add(project);

        //    client = new Client
        //    {
        //        Id = Guid.NewGuid(),
        //        Description = "Farmashop",
        //        RUT = "9999",
        //        Address = "Solano Lopez 1234",
        //        HourRates = new List<HourRate> {
        //            new HourRate
        //            {
        //                Id = Guid.NewGuid(),
        //                Description = "Horas estándar",
        //                Currency = Currency.Dollar,
        //                Rate = 1600,
        //                AdjustmentPeriodicity = Periodicity.Quarterly,
        //                StartingMonth = 1
        //            }
        //        }
        //    };
        //    context.Clients.Add(client);

        //    maintenance = new Maintenance
        //    {
        //        Id = Guid.NewGuid(),
        //        Description = "Mantenimiento Farmashop",
        //        Client = client,
        //        Amount = 7000,
        //        Currency = Currency.Dollar,
        //        Periodicity = Periodicity.Monthly
        //    };
        //    context.Maintenances.Add(maintenance);

        //    client = new Client
        //    {
        //        Id = Guid.NewGuid(),
        //        Description = "Chiappe",
        //        RUT = "1111",
        //        Address = "Rambla 1234",
        //        HourRates = new List<HourRate> {
        //            new HourRate
        //            {
        //                Id = Guid.NewGuid(),
        //                Description = "Horas estándar",
        //                Currency = Currency.Peso,
        //                Rate = 2000,
        //                AdjustmentPeriodicity = Periodicity.Annual,
        //                StartingMonth = 1
        //            }
        //        }
        //    };
        //    context.Clients.Add(client);

        //    maintenance = new Maintenance
        //    {
        //        Id = Guid.NewGuid(),
        //        Description = "Mantenimiento Chiappe",
        //        Client = client,
        //        Amount = 12000,
        //        Currency = Currency.Dollar,
        //        Periodicity = Periodicity.Biannual
        //    };
        //    context.Maintenances.Add(maintenance);

        //    client = new Client
        //    {
        //        Id = Guid.NewGuid(),
        //        Description = "Disco",
        //        RUT = "4321",
        //        Address = "Avda. Italia 1234",
        //        HourRates = new List<HourRate> {
        //            new HourRate
        //            {
        //                Id = Guid.NewGuid(),
        //                Description = "Horas estándar",
        //                Currency = Currency.Peso,
        //                Rate = 1400,
        //                AdjustmentPeriodicity = Periodicity.Quarterly,
        //                StartingMonth = 1
        //            }
        //        }
        //    };
        //    context.Clients.Add(client);

        //    maintenance = new Maintenance
        //    {
        //        Id = Guid.NewGuid(),
        //        Description = "Mantenimiento Disco",
        //        Client = client,
        //        Amount = 20000,
        //        Currency = Currency.Peso,
        //        Periodicity = Periodicity.Monthly
        //    };
        //    context.Maintenances.Add(maintenance);

        //    project = new Project
        //    {
        //        Id = Guid.NewGuid(),
        //        Description = "Migración WIS",
        //        Client = client,
        //        Amount = 120000,
        //        Currency = Currency.Dollar,
        //        Installments = 6,
        //        Fees = new List<Fee>
        //        {
        //            new Fee
        //            {
        //                Id = Guid.NewGuid(),
        //                Description = "Couta inicial",
        //                Month = 1,
        //                Amount = 1000,
        //                IVA = 220,
        //                Total = 2200
        //            },
        //            new Fee
        //            {
        //                Id = Guid.NewGuid(),
        //                Description = "Couta final",
        //                Month = 8,
        //                Amount = 10000,
        //                IVA = 2200,
        //                Total = 12200
        //            }
        //        }
        //    };
        //    context.Projects.Add(project);

        //    client = new Client
        //    {
        //        Id = Guid.NewGuid(),
        //        Description = "DFA",
        //        RUT = "1234",
        //        Address = "Rivera 1234",
        //        HourRates = new List<HourRate> {
        //            new HourRate
        //            {
        //                Id = Guid.NewGuid(),
        //                Description = "Horas estándar",
        //                Currency = Currency.Dollar,
        //                Rate = 60,
        //                AdjustmentPeriodicity = Periodicity.Quarterly,
        //                StartingMonth = 1
        //            }
        //        }
        //    };
        //    context.Clients.Add(client);

        //    maintenance = new Maintenance
        //    {
        //        Id = Guid.NewGuid(),
        //        Description = "Mantenimiento DFA",
        //        Client = client,
        //        Amount = 300000,
        //        Currency = Currency.Peso,
        //        Periodicity = Periodicity.Annual
        //    };
        //    context.Maintenances.Add(maintenance);
        //}
    }
}
