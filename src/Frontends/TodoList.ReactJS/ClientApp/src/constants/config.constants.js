const production = {
    url: {
        API_URL: 'https://localhost:7001', // Set production url (Gateway). 
    }
};

const development = {
    url: {
        API_URL: 'https://localhost:7001' // Local url (Gateway). 
    }
};

export const configConstants = process.env.NODE_ENV === 'development' ? development : production;
