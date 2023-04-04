const colors = require('tailwindcss/colors');
module.exports = {
  purge: {
    enabled: true,
    content: [
        '**/*.razor',
        '**/**/**/*.razor',
        '**/**/*.razor'
    ],
  },
  darkMode: false,
  theme:[],
  variants: {
    extend: {},
  },
  plugins: [],
}