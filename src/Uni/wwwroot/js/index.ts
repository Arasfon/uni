import flatpickr from "flatpickr";
import { Russian as flatpickrRu } from "flatpickr/dist/l10n/ru.js";

import MobileNavigation from "./navigation";

const mobileNavigation = new MobileNavigation();

// @ts-ignore
flatpickr("#datetime",
    {
        locale: flatpickrRu,
        allowInput: true,
        disableMobile: true,
        minDate: "today",
        enableTime: true,
        minTime: "10:00",
        maxTime: "20:00",
        minuteIncrement: 15,
        altInput: true,
        altFormat: "d.m.Y H:i"
    });

document.getElementById("bookForm")!.addEventListener("submit", event => {
    event.preventDefault();

    const form = event.target as HTMLFormElement;

    form.setAttribute("disabled", "");
    
    fetch(form.action, {
        method: "post",
        body: new FormData(form)
    }).then(() => window.location.href = "/visit/booking-thanks");
});