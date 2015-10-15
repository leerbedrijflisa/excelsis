export class Utils {

    doubleDigit(digit){
        return (digit.length < 2) ? "0" + digit : digit;
    }

    formatDate(date, time){
        var splitDate = date.split("-");
        var splitTime = time.split(":");

        return splitDate[2] + "-" + this.doubleDigit(splitDate[1]) + 
                              "-" + this.doubleDigit(splitDate[0]) +
                              "T" + this.doubleDigit(splitTime[0]) +
                              ":" + this.doubleDigit(splitTime[1]) + ":00Z";
    }
}