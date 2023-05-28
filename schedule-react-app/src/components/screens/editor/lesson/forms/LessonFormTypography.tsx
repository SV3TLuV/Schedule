import {Typography} from "@mui/material";

interface ILessonFormTypography {
    text: string
}

export const LessonFormTypography = ({text}: ILessonFormTypography) => {
    return (
        <Typography
            align="center"
            sx={{
                color: "#b0b3b8",
                height: "40px",
                fontWeight: "400",
                fontSize: "1rem",
                lineHeight: "40px",
                border: "1px solid #555555",
                borderRadius: "5px"
            }}
        >
            {text}
        </Typography>
    )
}