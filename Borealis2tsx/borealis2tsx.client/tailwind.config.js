/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        "./src/**/*.{html,js,jsx,ts,tsx}",
    ],
    theme: {
        'flex-basis': theme => ({
            '1': '100%',
            '2': '50%',
            '3': '33.333333%',
            '4': '25%',
            '5': '20%',
            '6': '16.666666%',
            '7': '14.285714%',
            '0': '12.5%',
        }),
        extend: {},
    },
    variants: {
        'flex-basis': ['responsive'],
    },
    plugins: [
        require('tailwind-bootstrap-grid')({
            containerMaxWidths: {
                sm: '540px',
                md: '720px',
                lg: '960px',
                xl: '1140px',
            },
        }),
        require('tailwindcss-grid')({
            grids: [2, 3, 5, 6, 8, 10, 12],
            gaps: {
                0: '0',
                4: '1rem',
                8: '2rem',
                '4-x': '1rem',
                '4-y': '1rem',
            },
            autoMinWidths: {
                '16': '4rem',
                '24': '6rem',
                '300px': '300px',
            },
            variants: ['responsive'],
        }),
        require('tailwindcss-gridlines'),
    ],
}