const scrollButton = document.getElementById("scroll-to-top") as HTMLButtonElement;

scrollButton.addEventListener("click", () => {
    window.scrollTo({
        top: 0,
        behavior: "smooth"
    });
});

document.addEventListener("scroll", () => {
    if (document.body.scrollTop > 100 || document.documentElement.scrollTop > 100) {
        scrollButton.setAttribute("show", "");
    } else {
        scrollButton.removeAttribute("show");
    }
});