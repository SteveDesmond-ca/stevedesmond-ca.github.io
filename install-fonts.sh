#!/usr/bin/env bash

mkdir -p assets/fonts/files
cp -r node_modules/@fontsource/roboto/latin.css assets/fonts/roboto.css
cp -r node_modules/@fontsource/noto-sans/latin.css assets/fonts/noto-sans.css
find node_modules/ -type f -regex '.*latin-[0-9]00.*\.woff2' | xargs -I % cp % assets/fonts/files

mkdir -p assets/fonts/fontawesome/css
cp -r node_modules/@fortawesome/fontawesome-free/css/all.min.css assets/fonts/fontawesome/css
cp -r node_modules/@fortawesome/fontawesome-free/webfonts assets/fonts/fontawesome
