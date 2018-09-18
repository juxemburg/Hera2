import { trigger, animate, transition, style, query, state } from '@angular/animations';

export const fadeInOutAnimation =
    trigger('animationState', [
        state('fade-out', style({
            opacity: 0
        })),
        state('fade-in', style({
            opacity: 1
        })),
        transition('fade-in <=> fade-out', animate('300ms ease'))
    ])

