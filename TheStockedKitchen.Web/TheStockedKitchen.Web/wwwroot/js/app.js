window.shouldNavBarBeOpen = function () {
    console.log(window.innerWidth)
    if (window.innerWidth < 1450) {
        console.log("return false")
        return false;
    }
    console.log("return true")
    return true;
};