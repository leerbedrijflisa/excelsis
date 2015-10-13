export class Utils {

    doubleDigit(digit){
        if (digit.length < 2)
        { 
            digit = "0" + digit;
        }
        return digit;
    }

    formatDate(date, time){
        var splitDate = date.split("-");
        var splitTime = time.split(":");

        return splitDate[2] + "-" + this.doubleDigit(splitDate[0]) + 
                              "-" + this.doubleDigit(splitDate[1]) +
                              "T" + this.doubleDigit(splitTime[0]) +
                              ":" + this.doubleDigit(splitTime[1]) + ":00Z";
    }
}