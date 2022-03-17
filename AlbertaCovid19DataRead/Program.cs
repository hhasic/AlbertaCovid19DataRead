using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AlbertaCovid19DataRead
{
    class Program
    {
        static int? GetNumberFromString(string numberString)
        {
            var returnVal = int.TryParse(Regex.Match(numberString, @"\d+").Value, out int number);
            return returnVal ? number : (int?)null;
        }
        static void ProcessCasesPage(string casesPage)
        {
            var tableData = GetValue(casesPage, "<tbody>", "</tbody>", casesPage.IndexOf("In Alberta the total number of cases"));
            var tableElements = tableData.Replace("\t","").Split("</tr>");
            var casesList = new List<Cases>();
            var now = DateTime.Now;
            for(var i = 0; i <= 7; i++)
            {
                var tableElement = tableElements[i].Replace(" ","").Replace(",","");
                var caseCounts = tableElement.Split("</td><td>");
                switch(i)
                {
                    case 0:
                        casesList.Add(new Cases
                        {
                            CasesArea = (int)CasesAreas.Canada,
                            Date = now,
                            Confirmed = GetNumberFromString(caseCounts[0]),
                            Active = GetNumberFromString(caseCounts[1]),
                            Recovered = GetNumberFromString(caseCounts[2]),
                            InHospital = GetNumberFromString(caseCounts[3]),
                            InIntensiveCare = GetNumberFromString(caseCounts[4]),
                            Deaths = GetNumberFromString(caseCounts[5]),
                            Tests = GetNumberFromString(caseCounts[6])
                        });
                        break;
                    case 1:
                        casesList.Add(new Cases
                        {
                            CasesArea = (int)CasesAreas.Alberta,
                            Date = now,
                            Confirmed = GetNumberFromString(caseCounts[0]),
                            Active = GetNumberFromString(caseCounts[1]),
                            Recovered = GetNumberFromString(caseCounts[2]),
                            InHospital = GetNumberFromString(caseCounts[3]),
                            InIntensiveCare = GetNumberFromString(caseCounts[4]),
                            Deaths = GetNumberFromString(caseCounts[5]),
                            Tests = GetNumberFromString(caseCounts[6])
                        });
                        break;
                    case 2:
                        casesList.Add(new Cases
                        {
                            CasesArea = (int)CasesAreas.CalgaryZone,
                            Date = now,
                            Confirmed = GetNumberFromString(caseCounts[0]),
                            Active = GetNumberFromString(caseCounts[1]),
                            Recovered = GetNumberFromString(caseCounts[2]),
                            InHospital = GetNumberFromString(caseCounts[3]),
                            InIntensiveCare = GetNumberFromString(caseCounts[4]),
                            Deaths = GetNumberFromString(caseCounts[5]),
                            Tests = GetNumberFromString(caseCounts[6])
                        });
                        break;
                    case 3:
                        casesList.Add(new Cases
                        {
                            CasesArea = (int)CasesAreas.EdmontonZone,
                            Date = now,
                            Confirmed = GetNumberFromString(caseCounts[0]),
                            Active = GetNumberFromString(caseCounts[1]),
                            Recovered = GetNumberFromString(caseCounts[2]),
                            InHospital = GetNumberFromString(caseCounts[3]),
                            InIntensiveCare = GetNumberFromString(caseCounts[4]),
                            Deaths = GetNumberFromString(caseCounts[5]),
                            Tests = GetNumberFromString(caseCounts[6])
                        });
                        break;
                    case 4:
                        casesList.Add(new Cases
                        {
                            CasesArea = (int)CasesAreas.CentralZone,
                            Date = now,
                            Confirmed = GetNumberFromString(caseCounts[0]),
                            Active = GetNumberFromString(caseCounts[1]),
                            Recovered = GetNumberFromString(caseCounts[2]),
                            InHospital = GetNumberFromString(caseCounts[3]),
                            InIntensiveCare = GetNumberFromString(caseCounts[4]),
                            Deaths = GetNumberFromString(caseCounts[5]),
                            Tests = GetNumberFromString(caseCounts[6])
                        });
                        break;
                    case 5:
                        casesList.Add(new Cases
                        {
                            CasesArea = (int)CasesAreas.SouthZone,
                            Date = now,
                            Confirmed = GetNumberFromString(caseCounts[0]),
                            Active = GetNumberFromString(caseCounts[1]),
                            Recovered = GetNumberFromString(caseCounts[2]),
                            InHospital = GetNumberFromString(caseCounts[3]),
                            InIntensiveCare = GetNumberFromString(caseCounts[4]),
                            Deaths = GetNumberFromString(caseCounts[5]),
                            Tests = GetNumberFromString(caseCounts[6])
                        });
                        break;
                    case 6:
                        casesList.Add(new Cases
                        {
                            CasesArea = (int)CasesAreas.NorthZone,
                            Date = now,
                            Confirmed = GetNumberFromString(caseCounts[0]),
                            Active = GetNumberFromString(caseCounts[1]),
                            Recovered = GetNumberFromString(caseCounts[2]),
                            InHospital = GetNumberFromString(caseCounts[3]),
                            InIntensiveCare = GetNumberFromString(caseCounts[4]),
                            Deaths = GetNumberFromString(caseCounts[5]),
                            Tests = GetNumberFromString(caseCounts[6])
                        });
                        break;
                    case 7:
                        casesList.Add(new Cases
                        {
                            CasesArea = (int)CasesAreas.Unknown,
                            Date = now,
                            Confirmed = GetNumberFromString(caseCounts[0]),
                            Active = GetNumberFromString(caseCounts[1]),
                            Recovered = GetNumberFromString(caseCounts[2]),
                            InHospital = GetNumberFromString(caseCounts[3]),
                            InIntensiveCare = GetNumberFromString(caseCounts[4]),
                            Deaths = GetNumberFromString(caseCounts[5]),
                            Tests = GetNumberFromString(caseCounts[6])
                        });
                        break;
                }
            }

            using (var context = new DataContext())
            {
                context.Cases.AddRange(casesList);
                context.SaveChanges();
            }
        }

        static void ProcessAreaArray(string areaArray, int type)
        {
            var now = DateTime.Now;
            var splitItems = areaArray.Split("\",\"");
            var areaList = new List<AreaCovidInfo>();
            foreach (var item in splitItems)
            {
                areaList.Add(new AreaCovidInfo
                {
                    Date = now,
                    Active = int.Parse(GetActive(item)),
                    Cases = int.Parse(GetCases(item)),
                    Deaths = int.Parse(GetDeaths(item)),
                    Recovered = int.Parse(GetRecovered(item)),
                    AreaId = GetOrAddAreaId(GetAreaName(item), type)
                });
            }

            using (var context = new DataContext())
            {
                context.AreaCovidInfo.AddRange(areaList);
                context.SaveChanges();
            }
        }

        static int GetOrAddAreaId(string areaName, int type)
        {
            using (var context = new DataContext())
            {
                var area = context.Area.SingleOrDefault(x => x.Name == areaName);
                if (area != null)
                {
                    return area.AreaId;
                }
                else
                {
                    var newArea = new Area()
                    {
                        Name = areaName,
                        StartDate = DateTime.Now,
                        EndDate = null,
                        AreaType = type
                    };

                    context.Area.Add(newArea);
                    context.SaveChanges();
                    return newArea.AreaId;
                }
            }
        }

        static string GetAreaName(string item)
        {
            var strString = "<strong>";
            var endString = "<\\/strong>";
            return GetValue(item, strString, endString);
        }

        static string GetCases(string item)
        {
            var strString = "strong><br/ > ";
            var endString = "Case";
            return GetValue(item, strString, endString);
        }

        static string GetActive(string item)
        {
            var strString = "Cases <br/> ";
            var endString = "Active";
            return GetValue(item, strString, endString);
        }

        static string GetRecovered(string item)
        {
            var strString = "Active <br/> ";
            var endString = "Recover";
            return GetValue(item, strString, endString);
        }

        static string GetDeaths(string item)
        {
            var strString = "Recovered <br/> ";
            var endString = "Death";
            return GetValue(item, strString, endString);
        }

        static void AddLabTestObject(string pageString)
        {
            var labDataStr = GetValue(pageString, "Table 7. Number of people tested for COVID-19 in Alberta by zone", "</table>");
            var newLabTesting = new LabTesting
            {
                Date = DateTime.Now,
                CalgaryZone = int.Parse(GetValue(labDataStr, "Calgary Zone</th><td>", "</td>").Replace(",", "")),
                EdmontonZone = int.Parse(GetValue(labDataStr, "Edmonton Zone</th><td>", "</td>").Replace(",", "")),
                NorthZone = int.Parse(GetValue(labDataStr, "North Zone</th><td>", "</td>").Replace(",", "")),
                CentralZone = int.Parse(GetValue(labDataStr, "Central Zone</th><td>", "</td>").Replace(",", "")),
                SouthZone = int.Parse(GetValue(labDataStr, "South Zone</th><td>", "</td>").Replace(",", "")),
                UnknownZone = int.Parse(GetValue(labDataStr, "Unknown</th><td>", "</td>").Replace(",", "")),
            };

            using (var context = new DataContext())
            {
                context.LabTesting.Add(newLabTesting);
                context.SaveChanges();
            }
        }

        static string GetValue(string item, string startString, string endString, int startIndex = 0)
        {
            var str = item.IndexOf(startString, startIndex);
            var end = item.IndexOf(endString, str);
            return item.Substring(str + startString.Length, end - (str + startString.Length));
        }

        async static Task<int> Main(string[] args)
        {
            //try
            //{
            //   Console.WriteLine("Hello World!");
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("https://www.alberta.ca/stats/covid-19-alberta-statistics.htm");
            var pageContents = await response.Content.ReadAsStringAsync();
            pageContents = pageContents.Replace("\n", "").Replace("\r", "");
            var startOfMunicipalitiesIndex = pageContents.IndexOf("[\"<strong>Municipal District Of Acadia No. 34");
            var endOfMunicipalitiesIndex = pageContents.IndexOf("]", startOfMunicipalitiesIndex);
            var municipalitiesArray = pageContents.Substring(startOfMunicipalitiesIndex, endOfMunicipalitiesIndex - startOfMunicipalitiesIndex + 1);
            var startOfLocalGeoAreasIndex = pageContents.IndexOf("[\"<strong>Crowsnest Pass");
            var endOfLocalGeoAreasIndex = pageContents.IndexOf("]", startOfLocalGeoAreasIndex);
            var localGeoAreasArray = pageContents.Substring(startOfLocalGeoAreasIndex, endOfLocalGeoAreasIndex - startOfLocalGeoAreasIndex + 1);


            ProcessAreaArray(municipalitiesArray, (int)AreaType.Municipality);
            ProcessAreaArray(localGeoAreasArray, (int)AreaType.LocalGeographicArea);
            AddLabTestObject(pageContents);

            response = await client.GetAsync("https://www.alberta.ca/covid-19-alberta-data.aspx");
            pageContents = await response.Content.ReadAsStringAsync();
            pageContents = pageContents.Replace("\n", "").Replace("\r", "");
            ProcessCasesPage(pageContents);
            // Console.ReadLine();
            return 0;
            //}
            //catch(Exception ex)
            //{
            //    var a = 2;
            //}
            //.WriteLine(pageContents);
          
        }
    }
}
