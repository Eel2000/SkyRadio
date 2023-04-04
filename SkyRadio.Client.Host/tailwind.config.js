const colors = require('tailwindcss/colors');
module.exports = {
  purge: {
    enabled: true,
    content: [
        './**/*.html',
        './**/*.razor'
    ],
  },
  darkMode: false,
  theme:[],
  variants: {
    extend: {},
  },
  plugins: [],
}