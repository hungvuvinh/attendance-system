import type { Config } from "tailwindcss";

const config: Config = {
	content: ["./src/pages/**/*.{js,ts,jsx,tsx,mdx}", "./src/components/**/*.{js,ts,jsx,tsx,mdx}", "./src/app/**/*.{js,ts,jsx,tsx,mdx}"],
	theme: {
		extend: {
			colors: {
				ink: "#132238",
				paper: "#f5f7f4",
				mint: "#b7e4c7",
				coral: "#ef8354"
			}
		}
	},
	plugins: []
};

export default config;
