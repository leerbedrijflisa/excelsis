using Lisa.Excelsis.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lisa.Excelsis.WebApi
{
    public class DummieData
    {
        public static List<Assessment> Assessments = new List<Assessment>();
        
        public static void LoadDummieData()
        {
            Random rnd = new Random();
            Assessments.Add(
                    new Assessment
                    {
                        Id = rnd.Next(1, 100),
                        ExamId = rnd.Next(1, 100),
                        TeacherId = rnd.Next(1, 100),
                        Criteria = new List<Criterium>()
                        {
                        new Criterium
                        {
                            Id = rnd.Next(1, 100),
                            MainValue = false,
                            CriteriumBoxes = new bool[]
                            {
                                false,true,false,false
                            }
                        },
                        new Criterium
                        {
                            Id = rnd.Next(1, 100),
                            MainValue = false,
                            CriteriumBoxes = new bool[]
                            {
                                false,true,false,false
                            }
                        },
                        new Criterium
                        {
                            Id = rnd.Next(1, 100),
                            MainValue = false,
                            CriteriumBoxes = new bool[]
                            {
                                false,true,false,false
                            }
                        }
                        }
                    }
                );
            Assessments.Add(
                new Assessment
                {
                    Id = rnd.Next(1, 100),
                    ExamId = rnd.Next(1, 100),
                    TeacherId = rnd.Next(1, 100),
                    Criteria = new List<Criterium>()
                     {
                        new Criterium
                        {
                            Id = rnd.Next(1, 100),
                            MainValue = false,
                            CriteriumBoxes = new bool[]
                            {
                                false,true,false,false
                            }
                        },
                        new Criterium
                        {
                            Id = rnd.Next(1, 100),
                            MainValue = false,
                            CriteriumBoxes = new bool[]
                            {
                                false,true,false,false
                            }
                        },
                        new Criterium
                        {
                            Id = rnd.Next(1, 100),
                            MainValue = false,
                            CriteriumBoxes = new bool[]
                            {
                                false,true,false,false
                            }
                        }
                     }
                }
            );
            Assessments.Add(
                new Assessment
                {
                    Id = rnd.Next(1, 100),
                    ExamId = rnd.Next(1, 100),
                    TeacherId = rnd.Next(1, 100),
                    Criteria = new List<Criterium>()
                     {
                        new Criterium
                        {
                            Id = rnd.Next(1, 100),
                            MainValue = false,
                            CriteriumBoxes = new bool[]
                            {
                                false,true,false,false
                            }
                        },
                        new Criterium
                        {
                            Id = rnd.Next(1, 100),
                            MainValue = false,
                            CriteriumBoxes = new bool[]
                            {
                                false,true,false,false
                            }
                        },
                        new Criterium
                        {
                            Id = rnd.Next(1, 100),
                            MainValue = false,
                            CriteriumBoxes = new bool[]
                            {
                                false,true,false,false
                            }
                        }
                     }
                }
            );            
        }
    }
}
