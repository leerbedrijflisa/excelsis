using Lisa.Excelsis.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lisa.Excelsis.WebApi
{
    public class DummieData
    {
        public static List<Exam> Exams = new List<Exam>();
        public static List<Assessment> Assessments = new List<Assessment>();
        
        public static void LoadDummieData()
        {
            ExamData();
            AssessmentsData();
        }
        public static void ExamData()
        {
            Exams.Add(new Exam
            {
                Id = 1,
                Name = "Lezen en Schrijven",
                Subject = "Nederlands",
                Organization = "Davinci College Dordrecht",
                Cohort = "2015/2016",
                DocumentationId = 1,
                questions = new List<Question>()
                {
                    new Question
                    {
                        Id = 0,
                        Description = "Vraag 1: Ik heb geen idee, heb jij wel een idee?",
                        Rating = 0
                        // 0 = Onvoldoende
                        // 1 = Voldoende
                        // 2 = Goed
                    },
                    new Question
                    {
                        Id = 1,
                        Description = "Vraag 2: Ik heb geen idee, heb jij wel een idee?",
                        Rating = 0
                        // 0 = Onvoldoende
                        // 1 = Voldoende
                        // 2 = Goed
                    },
                    new Question
                    {
                        Id = 2,
                        Description = "Vraag 3: Ik heb geen idee, heb jij wel een idee?",
                        Rating = 0
                        // 0 = Onvoldoende
                        // 1 = Voldoende
                        // 2 = Goed
                    }
                }
            });

            Exams.Add(new Exam
            {
                Id = 2,
                Name = "Iteratievergadering",
                Subject = "AO",
                Organization = "Davinci College Dordrecht",
                Cohort = "2015/2016",
                DocumentationId = 1,
                questions = new List<Question>()
                {
                    new Question
                    {
                        Id = 0,
                        Description = "Vraag 1: Ik heb geen idee, heb jij wel een idee?",
                        Rating = 0
                        // 0 = Onvoldoende
                        // 1 = Voldoende
                        // 2 = Goed
                    },
                    new Question
                    {
                        Id = 1,
                        Description = "Vraag 2: Ik heb geen idee, heb jij wel een idee?",
                        Rating = 0
                        // 0 = Onvoldoende
                        // 1 = Voldoende
                        // 2 = Goed
                    },
                    new Question
                    {
                        Id = 2,
                        Description = "Vraag 3: Ik heb geen idee, heb jij wel een idee?",
                        Rating = 0
                        // 0 = Onvoldoende
                        // 1 = Voldoende
                        // 2 = Goed
                    }
                }
            });

            Exams.Add(new Exam
            {
                Id = 3,
                Name = "Spreken",
                Subject = "Nederlands",
                Organization = "Davinci College Dordrecht",
                Cohort = "2015/2016",
                DocumentationId = 1,
                questions = new List<Question>()
                {
                    new Question
                    {
                        Id = 0,
                        Description = "Vraag 1: Ik heb geen idee, heb jij wel een idee?",
                        Rating = 0
                        // 0 = Onvoldoende
                        // 1 = Voldoende
                        // 2 = Goed
                    },
                    new Question
                    {
                        Id = 1,
                        Description = "Vraag 2: Ik heb geen idee, heb jij wel een idee?",
                        Rating = 0
                        // 0 = Onvoldoende
                        // 1 = Voldoende
                        // 2 = Goed
                    },
                    new Question
                    {
                        Id = 2,
                        Description = "Vraag 3: Ik heb geen idee, heb jij wel een idee?",
                        Rating = 0
                        // 0 = Onvoldoende
                        // 1 = Voldoende
                        // 2 = Goed
                    }
                }
            });
        }
        public static void AssessmentsData()
        {
            Assessments.Add(new Assessment
            {
                Id = 1,
                ExamId = 1,
                TeacherId = 85,
                Examinee = "Chery",
                Criteria = new List<Criterium>()
                {
                    new Criterium
                    {
                        Id = 0,
                        Answer = null,
                        CriteriumBoxes = new bool[]
                        {
                            false,false,false,false
                        }
                    },
                    new Criterium
                    {
                        Id = 1,
                        Answer = null,
                        CriteriumBoxes = new bool[]
                        {
                            false,false,false,false
                        }
                    },
                    new Criterium
                    {
                        Id = 2,
                        Answer = null,
                        CriteriumBoxes = new bool[]
                        {
                            false,false,false,false
                        }
                    }
                }
            });
            Assessments.Add(new Assessment
            {
                Id = 2,
                ExamId = 1,
                TeacherId = 3,
                Examinee = "Bob",
                Criteria = new List<Criterium>()
                {
                    new Criterium
                    {
                        Id = 4,
                        Answer = null,
                        CriteriumBoxes = new bool[]
                        {
                            false,false,false,false
                        }
                    },
                    new Criterium
                    {
                        Id = 5,
                        Answer = null,
                        CriteriumBoxes = new bool[]
                        {
                            false,false,false,false
                        }
                    },
                    new Criterium
                    {
                        Id = 6,
                        Answer = null,
                        CriteriumBoxes = new bool[]
                        {
                            false,false,false,false
                        }
                    }
                }
            });
            Assessments.Add(new Assessment
            {
                Id = 3,
                ExamId = 1,
                TeacherId = 7,
                Examinee = "Ron",
                Criteria = new List<Criterium>()
                {
                    new Criterium
                    {
                        Id = 0,
                        Answer = null,
                        CriteriumBoxes = new bool[]
                        {
                            false,false,false,false
                        }
                    },
                    new Criterium
                    {
                        Id = 1,
                        Answer = null,
                        CriteriumBoxes = new bool[]
                        {
                            false,false,false,false
                        }
                    },
                    new Criterium
                    {
                        Id = 2,
                        Answer = null,
                        CriteriumBoxes = new bool[]
                        {
                            false,false,false,false
                        }
                    }
                }
            });
        }
    }
}
