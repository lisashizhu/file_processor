using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using FileProcessor.Services.Interfaces;
using FileProcessor.Services.Models;
using FileProcessor.Services.Models.Mappers;
using FileProcessor.Data;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using FileProcessor.Common.Exceptions;
using System.Text.RegularExpressions;
using FileProcessor.Common.Extensions;

namespace FileProcessor.Services
{
    public class CsvFileProcessor : IProcess
    {
        private DatabaseContext _databaseContext;
        private readonly IMapper _mapper;
        private readonly IServiceProvider _serviceProvider;
        private DatabaseContext DatabaseContext => _databaseContext ??= _serviceProvider.GetRequiredService<DatabaseContext>();

        public CsvFileProcessor(IMapper mapper, IServiceProvider serviceProvider)
        {
            _mapper = mapper;
            _serviceProvider = serviceProvider;
        }

        public async Task SaveData(string csvString)
        {
            if (string.IsNullOrEmpty(csvString)) throw new BusinessException("Input is null or empty.");
            try
            {
                List<StoreOrderData> records = new List<StoreOrderData>();
                List<STORE_ORDER> dataToSave = new List<STORE_ORDER>();
                var config = new CsvConfiguration(CultureInfo.InvariantCulture);
                using var reader = new StringReader(csvString);
                using var csvReader = new CsvReader(reader, config);
                {
                    csvReader.Context.RegisterClassMap<StoreOrderDataMap>();
                    records = csvReader.GetRecords<StoreOrderData>().ToList();
                }
                dataToSave = records.Select(data => _mapper.Map<STORE_ORDER>(data))?.ToList();
                await DatabaseContext.AddRangeAsync(dataToSave);
                await DatabaseContext.SaveChangesAsync();
            }
            catch
            {
                throw new BusinessException("File cannot be saved!");
            }
        }
    }
}

