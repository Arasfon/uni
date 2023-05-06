import anime from "animejs";

function switchMenuOnOuterClick(e: Event) {
    const element = e.target;
    if (element instanceof HTMLElement && (element as HTMLElement).classList.contains("nav-menu__links-wrapper")) {
        switchMenu();
    }
}

function switchMenu() {
    const linksWrapper = document.querySelector(".nav-menu__links-wrapper") as HTMLElement;
    if (linksWrapper.classList.contains("nav-menu__links-wrapper--open")) {
        linksWrapper.classList.remove("nav-menu__links-wrapper--open");

        linksWrapper.removeEventListener("click", switchMenuOnOuterClick, true);

        document.body.style.removeProperty("overflow-y");

        anime({
            targets: linksWrapper,
            easing: "easeOutQuart",
            duration: 300,
            opacity: 0,
            complete: anim => {
                linksWrapper.style.removeProperty("opacity");
                linksWrapper.style.removeProperty("visibility");
            }
        });
    } else {
        linksWrapper.classList.add("nav-menu__links-wrapper--open");

        linksWrapper.style.visibility = "visible";

        document.body.style.overflowY = "hidden";

        linksWrapper.addEventListener("click", switchMenuOnOuterClick, true);

        anime({
            targets: linksWrapper,
            easing: "easeOutQuart",
            duration: 300,
            opacity: 1,
            complete: anim => {
                linksWrapper.style.visibility = "visible";
            }
        });

        anime({
            targets: ".nav-menu__links-wrapper a",
            easing: "easeOutQuart",
            duration: 300,
            translateX: ["-1rem", "0"],
            opacity: [0, 1],
            delay: anime.stagger(50)
        });
    }
}

document.querySelector(".nav-menu__menu-button").addEventListener("click", switchMenu);