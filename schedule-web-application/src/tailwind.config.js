/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ["./src/**/*.{js,jsx,ts,tsx}"],
    theme: {
        extend: {
            colors: {
                primaryDarkGray: "#222222",
                secondaryDarkGray: "#363636",
                primaryLightGray: "#e8e8e8",
                secondaryLightGray: "#c7c7c7",
                primaryBlue: "#17a9fd",
                secondaryBlue: "#0165E1",
                primaryRed: "#B05E5E",
                secondaryRed: "#940B0B",
                primaryYellow: "#dbc14d"
            },
            screens: {
                "none": "0px",
                "xs": "480px",
                "sm": "640px",
                "md": "768px",
                "lg": "1024px",
                "xl": "1280px",
                "2xl": "1536px",
            },
        },
    },
    plugins: [],
}
