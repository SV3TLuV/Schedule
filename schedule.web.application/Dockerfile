FROM node:18.16.0 as build
WORKDIR /app
COPY . .
RUN npm i
CMD ["npm", "run", "dev"]

#FROM nginx:1.24.0
#COPY --from=build /app/dist /usr/share/nginx/html
#RUN rm /etc/nginx/conf.d/default.conf
#COPY nginx/nginx.conf /etc/nginx/conf.d
#EXPOSE 3000
#CMD [ "nginx", "-g",  "daemon off;"]