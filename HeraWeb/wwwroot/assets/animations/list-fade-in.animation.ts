import { trigger, animate, transition, style, query, state, stagger } from '@angular/animations';

export const listAnimation =
    trigger('listAnimation', [
        transition('* <=> *', [
            query(
                ':enter',
                [
                    style({ opacity: 0, top: '110px', position: 'relative' }),
                    stagger(
                        '100ms',
                        animate(
                            '600ms cubic-bezier(.51,.33,.35,1.48)',
                            style({ opacity: 1, top: '0px', position: 'relative' })
                        )
                    )
                ],
                { optional: true }
            ),
            query(':leave', animate('50ms', style({ opacity: 0, transform: 'translateX(100%)' })), {
                optional: true
            })
        ])
    ]);
