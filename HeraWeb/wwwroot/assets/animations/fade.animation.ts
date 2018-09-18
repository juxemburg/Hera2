import { trigger, animate, transition, style, query, state } from '@angular/animations';

export const fadeAnimation =
    trigger('animationState', [
        state('in', style({ transform: 'translateY(0)', opacity: 1 })),
        transition('void => *', [
            style({ transform: 'translateY(20%)', opacity: 0 }),
            animate('600ms 300ms cubic-bezier(.51,.33,.35,1.48)')
        ]),
        transition('* => void', [
            animate('600ms cubic-bezier(.51,.33,.35,1.48)', style({ transform: 'translateX(100%)' }))
        ])
    ])

