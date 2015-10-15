export class Utils {

    doubleDigit(digit){
        return  (digit < 10) ? "0" + digit : digit;
    }

    formatDate(date, time){

        if(date.indexOf("/") > 1)
        {
            var splitDate = date.split("/");
        }
        else if(date.indexOf("-") > 1)
        {
            var splitDate = date.split("-");
        }
        else
        {
            alert("Gelieve de datum als 15-10-2015 of 15/10/2015 te noteren.")
        }
        
        var splitTime = time.split(":");

        return splitDate[2] + "-" + this.doubleDigit(splitDate[1]) + 
                              "-" + this.doubleDigit(splitDate[0]) +
                              "T" + this.doubleDigit(splitTime[0]) +
                              ":" + this.doubleDigit(splitTime[1]) + ":00Z";
    }
}