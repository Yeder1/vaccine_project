using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vaccination.BussinessLogic.DTOs;
using Vaccination.BussinessLogic.DTOs.VaccineDTOs;
using Vaccination.DataAccess.Models;
using Vaccination.DataAccess.Repositories;

namespace Vaccination.BussinessLogic.Services.Impl
{
    public class VaccineService : IBaseService<VaccineDTO>, IVaccineService
    {
        private readonly IVaccineRepository _vaccineRepository;
        private readonly IBaseRepository<VaccineType> _vaccineTypeRepository;
        private readonly IMapper _mapper;

        public VaccineService(IVaccineRepository vaccineRepository, IBaseRepository<VaccineType> vaccineTypeRepository, IMapper mapper)
        {
            _vaccineRepository = vaccineRepository;
            _vaccineTypeRepository = vaccineTypeRepository;
            _mapper = mapper;
        }

        public void Add(VaccineDTO entity)
        {
            var vaccineEntity = _mapper.Map<Vaccine>(entity);

            _vaccineRepository.Add(vaccineEntity);

            _vaccineRepository.Save();
        }

        public void Delete(VaccineDTO entity)
        {
            var vaccineEntity = _mapper.Map<Vaccine>(entity);

            _vaccineRepository.Delete(vaccineEntity);

            _vaccineRepository.Save();

        }

        public bool Deactivate(VaccineDTO entity)
        {
            var vaccineEntity = _mapper.Map<Vaccine>(entity);

            _vaccineRepository.Deactivate(vaccineEntity);

            _vaccineRepository.Save();
            return true;
        }

        public bool DeactivateMultipleVaccines(List<int> vaccineIds)
        {
            var vaccines = _vaccineRepository.GetAll().Where(v => vaccineIds.Contains(v.Id)).ToList();

            if (vaccines == null || !vaccines.Any())
            {
                return false; // Or throw an exception
            }

            foreach (var vaccine in vaccines)
            {
                _vaccineRepository.Deactivate(vaccine);
            }

            _vaccineRepository.Save();

            return true;
        }

        public void DeleteRange(List<VaccineDTO> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VaccineDTO> GetAll()
        {
            return _mapper.Map<IEnumerable<VaccineDTO>>(_vaccineRepository.GetAll().ToList());
        }

        public VaccineDTO GetById(int id)
        {
            return _mapper.Map<VaccineDTO>(_vaccineRepository.Find(v => v.Id == id));
        }

        public IEnumerable<VaccineDTO> GetVaccinesByTypes(int typeId)
        {
            return _mapper.Map<IEnumerable<VaccineDTO>>(_vaccineRepository.FindList(v => v.VaccineTypeId == typeId).ToList());
        }

        public IEnumerable<VaccineDTO> Search(string? keyword)
        {
            throw new NotImplementedException();
        }

        public void Update(VaccineDTO entity)
        {
            var vaccineEntity = _mapper.Map<Vaccine>(entity);

            _vaccineRepository.Update(vaccineEntity);

            _vaccineRepository.Save();
        }

        public (bool success, string errorMessage) ImportVaccineData(Stream fileStream)
        {
            try
            {
                using (var package = new ExcelPackage(fileStream))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    // Define the expected column headers
                    var expectedHeaders = new List<string>
                        {
                            "Active", "Vaccine name", "Vaccine type", "Usage", "Indication",
                            "Contraindication", "Number of injection", "Time of beginning next injection", "Time of ending next injection", "Origin"
                        };

                    // Read the first row headers
                    var actualHeaders = new List<string>();
                    for (int col = 1; col <= 10; col++)
                    {
                        actualHeaders.Add(worksheet.Cells[1, col].Text.Trim());
                    }

                    // Compare actual headers with expected headers
                    for (int i = 0; i < expectedHeaders.Count; i++)
                    {
                        if (!string.Equals(expectedHeaders[i].Trim(), actualHeaders[i].Trim(), StringComparison.OrdinalIgnoreCase))
                        {
                            return (false, "The given file doesn't match the sample file.");
                        }
                    }

                    var errorMessages = "";
                    var hasError = false;
                    if (rowCount < 2)
                    {
                        return (false, "No vaccine has been found.");
                    }

                    // validate and add each row of vaccine
                    for (int row = 2; row <= rowCount; row++)
                    {
                        var active = worksheet.Cells[row, 1].Text;
                        if (!active.Equals("TRUE") && !active.Equals("FALSE"))
                        {
                            errorMessages += $"Job status is invalid on line {row}.\n";
                            hasError = true;
                        }

                        var vaccineName = worksheet.Cells[row, 2].Text;
                        if (string.IsNullOrWhiteSpace(vaccineName))
                        {
                            errorMessages += $"Vaccine name is required on line {row}.\n";
                            hasError = true;
                        }

                        var vaccineTypeName = worksheet.Cells[row, 3].Text;
                        if (string.IsNullOrWhiteSpace(vaccineName))
                        {
                            errorMessages += $"Vaccine type name is required on line {row}.\n";
                            hasError = true;
                        }

                        var vaccineType = _vaccineTypeRepository.Find(vt => vt.VaccineTypeName.Equals(vaccineTypeName));
                        if (vaccineType == null)
                        {
                            errorMessages += $"Vaccine type '{vaccineTypeName}' does not exist in the database on line {row}.\n";
                            hasError = true;
                        }

                        var numberOfInjectionCell = worksheet.Cells[row, 7].Text;
                        if (!int.TryParse(numberOfInjectionCell, out _))
                        {
                            errorMessages += $"number of injection must be a valid number on line {row}.\n";
                            hasError = true;
                        }

                        var dateFormats = new[] { "MM/dd/yyyy", "M/d/yyyy" };

                        DateTime beginningNextInjection;
                        if (!DateTime.TryParseExact(worksheet.Cells[row, 8].Text, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out beginningNextInjection))
                        {
                            errorMessages += $"Invalid Time of beginning next injection on line {row}.\n";
                            hasError = true;
                        }

                        DateTime endingNextInjection;
                        if (!DateTime.TryParseExact(worksheet.Cells[row, 9].Text, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out endingNextInjection))
                        {
                            errorMessages += $"Invalid Time of ending next injection on line {row}.\n";
                            hasError = true;
                        }
                        else if (beginningNextInjection > endingNextInjection)
                        {
                            errorMessages += $"Time of ending next injection should be greater than Time of beginning next injection on line {row}.\n";
                            hasError = true;
                        }

                        if (hasError)
                        {
                            continue;
                        }

                        var newVaccine = new Vaccine
                        {
                            Status = active.Equals("TRUE") ? true : false,
                            VaccineName = vaccineName,
                            VaccineTypeId = vaccineType.Id,
                            Usage = worksheet.Cells[row, 4].Text,
                            Indication = worksheet.Cells[row, 5].Text,
                            Contraindication = worksheet.Cells[row, 6].Text,
                            NumberOfInjection = int.Parse(numberOfInjectionCell),
                            NextBeginNextInjection = beginningNextInjection,
                            NextEndNextInjection = endingNextInjection,
                            Origin = worksheet.Cells[row, 10].Text,
                        };

                        _vaccineRepository.Add(newVaccine);
                    }

                    if (hasError)
                    {
                        return (false, errorMessages);
                    }
                    _vaccineRepository.Save();
                }

                return (true, "");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return (false,e.Message);
            }
        }
    }
}
