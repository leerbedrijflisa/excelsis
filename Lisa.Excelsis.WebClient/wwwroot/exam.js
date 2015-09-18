export class Exam
{
    constructor() 
    {
        this.heading = "Exam";
        this.firstName = ;
        this.lastName;
        this.studentNumber;
    }

    //Mee bezig :

    activate(params) {
        return Promise.all([
            this.http.get('/api/jobs/' + params.id).then(http => {
                this.job = JSON.parse(http.response);
            }),
            this.http.get('/api/employees').then(http => {
                this.employees = JSON.parse(http.response);
            })
        ]);
    }
}