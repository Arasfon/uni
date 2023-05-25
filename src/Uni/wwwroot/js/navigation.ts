import anime from "animejs";

export default class MobileNavigation {
    private menuState = 0;
    private outerClickLambda = (e) => this.switchMenuOnOuterClick(e);

    constructor() {
        document.querySelector(".nav-menu__menu-button")!.addEventListener("click", () => this.switchMenu());
        for (let headerSwitch of document.querySelectorAll(".dropdown__header__switch")) {
            headerSwitch.addEventListener("click", (e) => this.switchDropdown(e, headerSwitch as HTMLElement, ((headerSwitch.parentNode as HTMLElement).parentNode as HTMLElement).querySelector(".dropdown__content") as HTMLElement));
        }
    }

    private switchMenuOnOuterClick(e: Event) {
        if ((e.target as HTMLElement).classList.contains("dropdown__header__switch") ||
            ((e.target as HTMLElement).parentElement as HTMLElement).classList.contains("dropdown__header__switch"))
            return;

        this.switchMenu();
    }

    switchMenu() {
        const linksWrapper = document.querySelector(".nav-menu__links-wrapper") as HTMLDivElement;
        if (linksWrapper.hasAttribute("open")) {
            linksWrapper.removeAttribute("open");

            linksWrapper.removeEventListener("click", this.outerClickLambda);

            document.body.classList.remove("no-scroll");

            this.menuState = 0;

            anime({
                targets: linksWrapper,
                easing: "easeOutQuart",
                duration: 300,
                opacity: 0,
                complete: () => {
                    if (this.menuState !== 0)
                        return;

                    linksWrapper.style.removeProperty("opacity");
                    linksWrapper.style.removeProperty("visibility");

                    const dropdowns = linksWrapper.querySelectorAll(".dropdown");
                    for (let dropdown of dropdowns) {
                        (dropdown.querySelector(".dropdown__content") as HTMLElement).removeAttribute("open");
                        (dropdown.querySelector(".dropdown__header__switch") as HTMLElement).removeAttribute("on");
                    }
                }
            });
        } else {
            linksWrapper.setAttribute("open", "");

            linksWrapper.style.visibility = "visible";
            document.body.classList.add("no-scroll");

            linksWrapper.addEventListener("click", this.outerClickLambda);

            this.menuState = 1;

            anime({
                targets: linksWrapper,
                easing: "easeOutQuart",
                duration: 300,
                opacity: 1,
                complete: () => {
                    if (this.menuState !== 1)
                        return;

                    linksWrapper.style.visibility = "visible";
                }
            });

            anime({
                targets: ".nav-menu__links-wrapper > *",
                easing: "easeOutQuart",
                duration: 300,
                translateX: ["-1rem", "0"],
                opacity: [0, 1],
                delay: anime.stagger(50)
            });
        }
    }

    switchDropdown(e: Event, source: HTMLElement, content: HTMLElement) {
        e.preventDefault();

        if (content.hasAttribute("open")) {
            content.removeAttribute("open");
            source.removeAttribute("on");
        } else {
            content.setAttribute("open", "");
            source.setAttribute("on", "");

            anime({
                targets: content.children,
                easing: "easeOutQuart",
                duration: 300,
                translateX: ["-1rem", "0"],
                opacity: [0, 1],
                delay: anime.stagger(50)
            });
        }
    }
}